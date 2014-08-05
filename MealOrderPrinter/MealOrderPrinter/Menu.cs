using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealOrderPrinter
{
    public static class Menu
    {
        public static Dictionary<DishType, string> MorningDishes;

        public static Dictionary<DishType, string> NightDishes;

        static Menu()
        {
            MorningDishes = new Dictionary<DishType, string>();
            MorningDishes.Add(DishType.entree, "eggs");
            MorningDishes.Add(DishType.side, "Toast");
            MorningDishes.Add(DishType.drink, "coffee");


            NightDishes = new Dictionary<DishType, string>();
            NightDishes.Add(DishType.entree, "steak");
            NightDishes.Add(DishType.side, "potato");
            NightDishes.Add(DishType.drink, "wine");
            NightDishes.Add(DishType.dessert, "cake");
        }

        public static string GetMenuItem(bool isMorning, DishType dishtype)
        {
            if (isMorning)
                return MorningDishes[dishtype];
            if (!isMorning)
                return NightDishes[dishtype];
            return string.Empty;
        }
    }
}
