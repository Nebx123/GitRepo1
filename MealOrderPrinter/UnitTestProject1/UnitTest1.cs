using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MealOrderPrinter;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void dessertNotapplicablewhenmorning()
        {
            var inputReader = new InputProcessor();
            inputReader.IsMorningSelected = true;
            var dishtype = DishType.dessert;

            var notapplicable = inputReader.ValidateInput(dishtype);
            Assert.AreEqual(notapplicable, false);
        }


        [TestMethod]
        public void AllowMultibleSelectionOfCoffeeIntheMorning()
        {
            var inputReader = new InputProcessor();
            inputReader.IsMorningSelected = true;
            var coffee = DishType.drink;

            inputReader.AddSelection( coffee);
            inputReader.AddSelection( coffee);
            Assert.AreEqual(inputReader.DishSelectionOutput.GetDishCount(coffee), 2);
        }

        [TestMethod]
        public void AllowMultibleSelectionOfPotatoIntheNight()
        {
            var inputReader = new InputProcessor();
            inputReader.IsMorningSelected = false ;
            var potato = DishType.side;

            inputReader.AddSelection( potato);
            inputReader.AddSelection( potato);
            Assert.AreEqual(inputReader.DishSelectionOutput.GetDishCount(potato), 2);
        }

        [TestMethod]
        public void AllowOnlySingleSelectionOfDishes()
        {
            var inputReader = new InputProcessor();
            inputReader.IsMorningSelected = true;
            var eggs = DishType.entree;

            inputReader.AddSelection( eggs);
            inputReader.AddSelection( eggs);
            inputReader.AddSelection( eggs);
            Assert.AreEqual(inputReader.DishSelectionOutput.GetDishCount(eggs), 1);
        }


        [TestMethod]
        public void NoDessertInTheMorning_ErrorOccurred_Status_to_True()
        {
            var inputReader = new InputProcessor();
            inputReader.IsMorningSelected = true;
            var dishtype = DishType.dessert;

            inputReader.AddSelection( dishtype);
            Assert.AreEqual(inputReader.ErrorOccurred, true);
        }

        [TestMethod]
        public void MultipleDish_of_SameType_Raise_Error()
        {
            var inputReader = new InputProcessor();
            inputReader.IsMorningSelected = true;
            var dishtype = DishType.entree;

            inputReader.AddSelection(dishtype);
            inputReader.AddSelection(dishtype);

            Assert.AreEqual(inputReader.ErrorOccurred, true);
        }


        [TestMethod]
        public void TestInitialStatusOf_InputReader()
        {
            var inputReader = new InputProcessor();

            Assert.AreEqual(inputReader.ErrorOccurred, false);
            Assert.IsTrue(inputReader.DishSelectionOutput.GetDishCount(DishType.dessert) == 0);
            Assert.IsTrue(inputReader.DishSelectionOutput.GetDishCount(DishType.drink) == 0);
            Assert.IsTrue(inputReader.DishSelectionOutput.GetDishCount(DishType.entree) == 0);
            Assert.IsTrue(inputReader.DishSelectionOutput.GetDishCount(DishType.side) == 0);

            Assert.IsTrue(inputReader.DishSelectionInput.Count == 0);
        }


        [TestMethod]
        public void SetTimeOfDayAsNightAndMorning()
        {
            var inputReader = new InputProcessor();

            inputReader.SetTimeOfDay( "night");

            Assert.AreEqual(inputReader.IsMorningSelected, false);
        }


        [TestMethod]
        public void InvalidInput_DishType_SetsErrorOccurredFlag()
        {
            var dishtype = "23";

            var inputReader = new InputProcessor();
            DishType type = inputReader.ReadDishType(dishtype);

            Assert.AreEqual(inputReader.ErrorOccurred, true);
        }

        [TestMethod]
        public void NoDishSelection_Has_No_PrintValue()
        {
            var dishtype = "3";

            var inputReader = new InputProcessor();
            inputReader.ProcessInput("morning");
            var printValues = inputReader.PrintOutput();

            Assert.AreEqual(printValues, "");
        }


        [TestMethod]
        public void MultipleDishSelection_prints_Count()
        {
            var dishtype = "3";

            var inputReader = new InputProcessor();
            inputReader.ProcessInput("morning, 3, 3, 3, 3");
            var printValues = inputReader.PrintOutput();

            Assert.AreEqual(printValues, "coffee(x4)");
        }

        [TestMethod]
        public void TestRandomInputs1()
        {
            var input = "night, 1, 2, 3, 5";
            var inputReader = new InputProcessor();

            inputReader.ProcessInput(input);

            var printValues = inputReader.PrintOutput();

            Assert.AreEqual(printValues, "steak, potato, wine, error");
        }

        [TestMethod]
        public void TestRandomInputs2()
        {
            var input = "night, 1, 1, 2, 3, 5";

            var inputReader = new InputProcessor();

            inputReader.ProcessInput(input);

            var printValues = inputReader.PrintOutput();

            Assert.AreEqual(printValues, "steak, error");
        }

        [TestMethod]
        public void TestRandomInputs3()
        {
            var input = "night, 1, 2, 2, 4";

            var inputReader = new InputProcessor();

            inputReader.ProcessInput(input);

            var printValues = inputReader.PrintOutput();

            Assert.AreEqual(printValues, "steak, potato(x2), cake");
        }

        [TestMethod]
        public void TestRandomInputs4()
        {
            var input = "morning, 1, 2, 3, 3, 3";

            var inputReader = new InputProcessor();

            inputReader.ProcessInput(input);

            var printValues = inputReader.PrintOutput();

            Assert.AreEqual(printValues, "eggs, Toast, coffee(x3)");
        }//morning, 1, 2, 3, 4

        [TestMethod]
        public void TestRandomInputs5()
        {
            var input = "morning, 1, 2, 3, 4";

            var inputReader = new InputProcessor();

            inputReader.ProcessInput(input);

            var printValues = inputReader.PrintOutput();

            Assert.AreEqual(printValues, "eggs, Toast, coffee, error");
        }//
    }
}
