using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaProject.Controllers
{

    public class PizzaJson
    {
        public int id;
        public string name;
        public int price;
        public int category;
        public int rating;
        public List<int> types = new List<int>();
        public List<int> sizes = new List<int>();
        public List<string> imageUrls = new List<string>();
    }


    [Route("api/pizza")]
    [ApiController]
    public class HomeController : Controller
    {

        private PizzaContext? _db;

        public HomeController(PizzaContext pizzaContext)
        {
            _db = pizzaContext;
        }


        // GET api/pizza
        // Получение списка товаров
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JsonResult>>> Get(string category, string _sort)
        {
            try
            {
                List<PizzaJson> pizzas = new List<PizzaJson>();
                List<PizzaImage> images = await _db.PizzaImages.ToListAsync();
                List<Models.Type> allTypes = await _db.Types.ToListAsync();
                List<Pizza_Type> pts = await _db.Pizza_Types.ToListAsync();
                await _db.Pizzas.ForEachAsync(pizza =>
                {
                    PizzaJson curPizza = new PizzaJson();
                    curPizza.id = pizza.Id;
                    curPizza.name = pizza.Name;
                    curPizza.price = pizza.Price;
                    curPizza.category = pizza.Category;
                    curPizza.rating = pizza.Rating;

                    foreach (Pizza_Type pt in pts)
                    {
                        if (pt.PizzaId == pizza.Id)
                        {
                            curPizza.types.Add(pt.Type.TypeValue);
                        }
                    }
                    foreach (PizzaImage img in images)
                    {
                        if (img.PizzaId == pizza.Id)
                        {
                            curPizza.imageUrls.Add(img.ImageUrl);
                            curPizza.sizes.Add(img.Size);
                        }
                    }
                    pizzas.Add(curPizza);
                });

                // Если нет никаких параметров запроса, то сразу возвращаем результат, чтобы ускорить процесс
                if (category == null && _sort == null)
                {
                    return new JsonResult(pizzas);
                }

                var result = from s in pizzas select s;

                if (category != null)
                {
                    result = result.Where(el => Convert.ToString(el.category) == category);

                }

                if (_sort != null)
                {
                    switch (_sort)
                    {
                        case "popular":
                            {
                                result = result.OrderBy(el => el.rating);
                                break;
                            }

                        case "price":
                            {
                                result = result.OrderBy(el => el.price);
                                break;
                            }
                        case "name":
                            {
                                result = result.OrderBy(el => el.name);
                                break;
                            }
                    }
                }


                return new JsonResult(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST api/pizza
        // Создание товара
        [HttpPost]
        public async Task<ActionResult<Pizza>> Post(PizzaJson pizza)
        {
            if (pizza == null)
            {
                return BadRequest();
            }

            List<Models.Type> allTypes = await _db.Types.ToListAsync();

            Pizza newPizza = new Pizza();
            newPizza.Name = pizza.name;
            newPizza.Price = pizza.price;
            newPizza.Rating = pizza.rating;
            newPizza.Category = pizza.category;



            for (int i = 0; i < pizza.imageUrls.Count; i++)
            {
                PizzaImage pizzaImage = new PizzaImage()
                {
                    ImageUrl = pizza.imageUrls[i],
                    Size = pizza.sizes[i],
                };

                newPizza.Images.Add(pizzaImage);
            }

            foreach (int i in pizza.types)
            {
                Pizza_Type pt = new Pizza_Type()
                {
                    PizzaId = pizza.id,
                    Type = allTypes.FirstOrDefault(el => el.TypeValue == i)
                };

                newPizza.Pizza_Types.Add(pt);
            }

            await _db.Pizzas.AddAsync(newPizza);
            await _db.SaveChangesAsync();

            return Ok(newPizza);
        }


        // Обновление товара
        // PUT api/pizza
        [HttpPut("{id}")]
        public async Task<ActionResult<Pizza>> Put(PizzaJson pizza)
        {
            if (pizza == null)
            {
                return BadRequest();
            }

            Pizza foundPizza = await _db.Pizzas.FirstOrDefaultAsync(el => el.Id == pizza.id);

            if (foundPizza == null)
            {
                return NotFound();
            }

            foundPizza.Name = pizza.name;
            foundPizza.Price = pizza.price;
            foundPizza.Rating = pizza.rating;
            foundPizza.Category = pizza.category;


            List<PizzaImage> images = await _db.PizzaImages.ToListAsync();

            // Удаляем все связанные изображения
            foundPizza.Images.Clear();

            // Создаем новые связанные изображения
            for (int i = 0; i < pizza.imageUrls.Count; i++)
            {
                PizzaImage pizzaImage = new PizzaImage()
                {
                    ImageUrl = pizza.imageUrls[i],
                    Size = pizza.sizes[i],
                };

                foundPizza.Images.Add(pizzaImage);
            }


            await _db.SaveChangesAsync();
            return Ok(pizza);
        }


        // DELETE api/pizza
        // Удаление товара
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pizza>> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Pizza pizza = _db.Pizzas.FirstOrDefault(el => el.Id == id);

            if (pizza == null)
            {
                return NotFound();
            }

            _db.Pizzas.Remove(pizza);
            await _db.SaveChangesAsync();
            return Ok(pizza);
        }


    }
}
