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
        public short price;
        public short category;
        public short rating;
        public List<int> types;
        public List<int> sizes;
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
        public async Task<ActionResult<IEnumerable<JsonResult>>> Get()
        {
            List<PizzaJson> result = new List<PizzaJson>();
            List<Image> images = await _db.Image.ToListAsync();

            await _db.Pizza.ForEachAsync(pizza =>
            {
                PizzaJson curPizza = new PizzaJson();
                curPizza.id = pizza.Id;
                curPizza.name = pizza.Name;
                curPizza.price = pizza.Price;
                curPizza.category = pizza.Category;
                curPizza.rating = pizza.Rating;
                curPizza.types = pizza.Types.Split(",").Select(el => Convert.ToInt32(el)).ToList();
                curPizza.sizes = pizza.Sizes.Split(",").Select(el => Convert.ToInt32(el)).ToList();
                foreach (Image img in images)
                {
                    if (img.PizzaId == pizza.Id)
                    {
                        curPizza.imageUrls.Add(img.ImageUrl);
                    }
                }
                result.Add(curPizza);
            });

            return new JsonResult(result);
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
            Pizza newPizza = new Pizza();
            newPizza.Name = pizza.name;
            newPizza.Price = pizza.price;
            newPizza.Rating = pizza.rating;
            newPizza.Category = pizza.category;
            newPizza.Types = string.Join(",", pizza.types);
            newPizza.Sizes = string.Join(",", pizza.sizes);

            await _db.Pizza.AddAsync(newPizza);

            foreach (string url in pizza.imageUrls)
            {
                Image img = new Image();
                img.ImageUrl = url;
                img.PizzaId = newPizza.Id;

                await _db.Image.AddAsync(img);

                newPizza.Image.Add(img);
            }

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

            Pizza foundPizza = await _db.Pizza.FirstOrDefaultAsync(el => el.Id == pizza.id);

            if (foundPizza == null)
            {
                return NotFound();
            }

            foundPizza.Name = pizza.name;
            foundPizza.Price = pizza.price;
            foundPizza.Rating = pizza.rating;
            foundPizza.Category = pizza.category;
            foundPizza.Types = string.Join(",", pizza.types);
            foundPizza.Sizes = string.Join(",", pizza.sizes);

            await _db.SaveChangesAsync();
            return Ok(pizza);
        }


        // DELETE api/pizza
        // Удаление товара
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pizza>> Delete(int id)
        {
            Pizza pizza = _db.Pizza.FirstOrDefault(el => el.Id == id);

            if (pizza == null)
            {
                return NotFound();
            }

            _db.Pizza.Remove(pizza);
            await _db.SaveChangesAsync();
            return Ok(pizza);
        }


    }
}
