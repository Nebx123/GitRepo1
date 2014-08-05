using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealOrderPrinter
{    

    public class InputProcessor
    {
        public bool? IsMorningSelected;
        public List<DishType> DishSelectionInput;

        public bool ErrorOccurred;
        public DishSelection DishSelectionOutput;

        public InputProcessor()
        {
            Initialize();
        }

        public void Initialize()
        {
            DishSelectionOutput = new DishSelection();

            DishSelectionInput = new List<DishType>();

            ErrorOccurred = false;

            IsMorningSelected = (bool?)null;
        }

        public void ProcessInput(string inputstring)
        {
            var parameters = inputstring.Split(',').ToList();

            if (parameters.Any())
            {
                SetTimeOfDay(parameters[0]);

                if (!ErrorOccurred)
                {
                    for (var i = 1; i < parameters.Count() && !ErrorOccurred; i++)
                    {
                        var inputDish = ReadDishType(parameters[i]);
                        if (!ErrorOccurred)
                        {
                            AddSelection(inputDish);
                        }
                        else
                            return;
                    }
                }
                return;
            }
        }

        public bool IsMorning()
        {
            return IsMorningSelected.HasValue && IsMorningSelected.Value;
        }

        public bool IsNight()
        {
            return IsMorningSelected.HasValue && !IsMorningSelected.Value;;
        }

        public bool ValidateInput( DishType dishtype)
        {
            if (IsMorning())
            {
                if (dishtype == DishType.dessert) return false;
            }
            return true;
        }

        public void AddSelection( DishType dishtype)
        {
            var validInput = ValidateInput(dishtype);

            if (validInput)
            {
                if (DishSelectionOutput.GetDishCount(dishtype) == 0)
                {
                    DishSelectionOutput.IncrementDishCount(dishtype);
                    return;
                }

                if (IsMorning() && dishtype == DishType.drink)
                {
                    DishSelectionOutput.IncrementDishCount(dishtype);
                    return;
                }
                if (IsNight() && dishtype == DishType.side)
                {
                    DishSelectionOutput.IncrementDishCount(dishtype);
                    return;
                }

                ErrorOccurred = true;
            }
            else
            {
                ErrorOccurred = true;
            }
        }



        public void SetTimeOfDay(string timeofday)
        {
            if (string.Compare(timeofday, "night", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.IsMorningSelected = false;
            }
            else
                if (string.Compare(timeofday, "morning", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.IsMorningSelected = true;
                }
                else
                    this.ErrorOccurred = true;
        }


        public DishType ReadDishType(string inputdish)
        {
            try 
            {
                var dishType = (DishType)Enum.Parse(typeof(DishType), inputdish);
                
                var isDishDefined = Enum.IsDefined(typeof(DishType), dishType);
                
                if(isDishDefined)
                    return dishType;
                else
                    ErrorOccurred = true;
            }
            catch (Exception ex) {
                ErrorOccurred = true;
            }
            return DishType.NotApplicable;
        }

        public string PrintOutput()
        {
            var dishtypes = Enum.GetValues(typeof(DishType))
                             .Cast<DishType>()
                             .Where(d => d != DishType.NotApplicable)
                             .ToArray();

            Array.Sort(dishtypes);

            var printvalues = new List<string>();

            foreach(var dishtype in dishtypes)
            {
                var print = GetPrint(dishtype);
                if( !string.IsNullOrEmpty(print) )
                    printvalues.Add(print);
            }

            if (ErrorOccurred)
                printvalues.Add("error");

            return string.Join(", ", printvalues);
        }

        private string GetPrint(DishType dishtype)
        {
            if ( DishSelectionOutput.GetDishCount(dishtype) == 0 )
                return string.Empty;

            if (DishSelectionOutput.GetDishCount(dishtype) == 1)
                return Menu.GetMenuItem(IsMorningSelected.Value, dishtype);

            return string.Format("{0}(x{1})", Menu.GetMenuItem(IsMorningSelected.Value, dishtype), DishSelectionOutput.GetDishCount(dishtype));
        }


    }
}
