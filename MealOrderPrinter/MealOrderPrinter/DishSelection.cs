using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealOrderPrinter
{
    public class DishSelection
    {
        private Dictionary<DishType, int> DishSelectionOutput;
        public DishSelection()
        {
            DishSelectionOutput = new Dictionary<DishType, int>();

            DishSelectionOutput.Add(DishType.entree, 0);
            DishSelectionOutput.Add(DishType.side, 0);
            DishSelectionOutput.Add(DishType.drink, 0);
            DishSelectionOutput.Add(DishType.dessert, 0);
        }

        public int GetDishCount(DishType dishtype)
        {
            return DishSelectionOutput[dishtype];
        }

        public void IncrementDishCount(DishType dishtype)
        {
            DishSelectionOutput[dishtype]++;
        }
    }
}
