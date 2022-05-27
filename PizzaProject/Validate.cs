using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaProject
{
    public static class Validate
    {
        public static string CreateValidatoin(string name = "", int price = 0, int category = 0, int rating = 0, List<int> sizes = null, List<int> types = null, List<string> imageUrls = null)
        {
            if (name.Length < 2)
            {
                return "Длина названия должна быть больше 2х символов";
            }

            if (price < 0)
            {
                return "Цена не может быть отрицательной";
            }

            if (category < 0)
            {
                return "Категория не может быть отрицательной";
            }

            if (rating < 0)
            {
                return "Рейтинг не может быть отрицательным";
            }

            if (rating > 10)
            {
                return "Рейтинг не может быть больше 10";
            }

            if (sizes.Count == 0)
            {
                return "У товара должны быть указаны размеры!";
            }
            else
            {
                if (!(sizes.Contains(26) || sizes.Contains(30) || sizes.Contains(40))) {
                    return "Задан несуществующий размер!";
                }
                
            }

            if (types.Count == 0)
            {
                return "У товара должны быть указаны типы!";
            }
            else
            {
                if (!(types.Contains(0) || types.Contains(1)))
                {
                    return "Задан несуществующий тип!";
                }
            }

            if (imageUrls.Count == 0)
            {
                return "У товара должно быть хотя бы одно изображение!";
            }


            return "Успешно";
        }
    }
}
