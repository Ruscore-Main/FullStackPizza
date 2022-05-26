using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaProject
{
    public static class Validate
    {
        public static string CreateValidatoin(string name, int price, int category, int rating)
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

            return "Успешно";
        }
    }
}
