using CashFlow.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CashFlow.Entity;
using System.Collections.Generic;

namespace CashFlow.Tests
{
    
    
    /// <summary>
    ///This is a test class for RepositoryTest and is intended
    ///to contain all RepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RepositoryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetExpenses
        ///</summary>
        [TestMethod()]
        public void GetExpensesTest()
        {
            Repository target = new Repository();
            DateTime start = new DateTime(2014, 10, 16);
            DateTime end = new DateTime(2014, 10, 17);            
            List<Expense> actual;
            actual = target.GetExpenses(start, end);
            Assert.AreNotEqual(0, actual.Count);
            
        }

        /// <summary>
        ///A test for GetIncome
        ///</summary>
        [TestMethod()]
        public void GetIncomeTest()
        {
            Repository target = new Repository();
            DateTime start = new DateTime(2014, 10, 16);
            DateTime end = new DateTime(2014, 10, 17);
            List<Income> actual;
            actual = target.GetIncome(start, end);
            Assert.AreNotEqual(0, actual.Count);
            
        }

        /// <summary>
        ///A test for InsertExpense
        ///</summary>
        [TestMethod()]
        public void InsertExpenseTest()
        {
            Repository target = new Repository();
            Expense expense = new Expense() { Amount = 10m, Description = "Fred Meyer", ExpenseCategoryID = 5, ExpenseDate = DateTime.Now, ExpenseTypeID = 1 };
            int expected = 0; 
            int actual;
            actual = target.InsertExpense(expense);
            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///A test for InsertIncome
        ///</summary>
        [TestMethod()]
        public void InsertIncomeTest()
        {
            Repository target = new Repository();
            Income income = new Income() { Amount = 1000m, PaymentDate = DateTime.Now, Source = "iLink-Systems" };
            int expected = 0; 
            int actual;
            actual = target.InsertIncome(income);
            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetExpenseTypes
        ///</summary>
        [TestMethod()]
        public void GetExpenseTypesTest()
        {
            Repository target = new Repository(); 
            List<ExpenseType> actual;
            actual = target.GetExpenseTypes();
            Assert.AreNotEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for GetExpenseCategories
        ///</summary>
        [TestMethod()]
        public void GetExpenseCategoriesTest()
        {
            Repository target = new Repository(); 
            List<ExpenseCategory> actual;
            actual = target.GetExpenseCategories();
            Assert.AreNotEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for GetExpenseAutocomplete
        ///</summary>
        [TestMethod()]
        public void GetExpenseAutocompleteTest()
        {
            Repository target = new Repository();
            Expense expense = new Expense() { Description = "Fred Meyer", ExpenseCategoryID = 5 };
            
            string[] actual;
            actual = target.GetExpenseAutocomplete(expense);
            Assert.AreNotEqual(0, actual.Length);
        }

        /// <summary>
        ///A test for GetIncomeAutocomplete
        ///</summary>
        [TestMethod()]
        public void GetIncomeAutocompleteTest()
        {
            Repository target = new Repository();
            string text = "iLink";
            string[] actual;
            actual = target.GetIncomeAutocomplete(text);
            Assert.AreNotEqual(0, actual.Length);
        }
    }
}
