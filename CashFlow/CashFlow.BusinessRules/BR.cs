using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CashFlow.DataAccess;
using CashFlow.Entity;
using Logging;

namespace CashFlow.BusinessRules
{
    public class BR
    {
        private IRepository repository;

        /// <summary>
        /// Constructor. 
        /// </summary>
        /// <param name="repos"> Repository interface. </param>
        public BR(IRepository repos)
        {
            this.repository = repos;
        }

        /// <summary>
        /// Gets current expenses. 
        /// </summary>
        /// <returns> A list of current expenses. </returns>
        public List<Expense> GetCurrentExpenses()
        {
            LogHelper.LogInfo("Entering BR.GetCurrentExpenses.");

            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime end = DateTime.Now + new TimeSpan(1, 0, 0, 0);

            List<Expense> expenses = this.repository.GetExpenses(start, end);

            LogHelper.LogInfo("Exiting BR.GetCurrentExpenses.");
            return expenses;
        }

        /// <summary>
        /// Gets current income. 
        /// </summary>
        /// <returns> List of current income items. </returns>
        public List<Income> GetCurrentIncome()
        {
            LogHelper.LogInfo("Entering BR.GetCurrentIncome.");

            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime end = DateTime.Now + new TimeSpan(1, 0, 0, 0);

            List<Income> income = this.repository.GetIncome(start, end);

            LogHelper.LogInfo("Exiting BR.GetCurrentIncome.");
            return income;
        }

        /// <summary>
        /// Records an expense. 
        /// </summary>
        /// <param name="expense"> Expense entity. </param>
        /// <returns> Expense id. </returns>
        public int RecordExpense(Expense expense)
        {
            LogHelper.LogInfo("Entering BR.RecordExpense.");

            expense.ID = this.repository.InsertExpense(expense);

            LogHelper.LogInfo(string.Format("Exiting BR.RecordExpense with expense.ID = {0}.", expense.ID.ToString()));
            return expense.ID;
        }

        /// <summary>
        /// Records income. 
        /// </summary>
        /// <param name="income"> Income object. </param>
        /// <returns> ID of income object. </returns>
        public int RecordIncome(Income income)
        {
            LogHelper.LogInfo("Entering BR.RecordIncome.");

            income.ID = this.repository.InsertIncome(income);

            LogHelper.LogInfo(string.Format("Exiting BR.RecordIncome with income.ID = {0}.", income.ID.ToString()));
            return income.ID;
        }

        /// <summary>
        /// Summarizes cash flow for a given period. 
        /// </summary>
        /// <param name="start"> Start date. </param>
        /// <param name="end"> End date. </param>
        /// <returns> History object. </returns>
        public History GetHistory(DateTime start, DateTime end)
        {
            LogHelper.LogInfo(string.Format("Entering BR.GetHistory with start = {0} and end = {1}.", start.ToString(), end.ToString()));

            List<Expense> expenses = this.repository.GetExpenses(start, end);
            List<Income> income = this.repository.GetIncome(start, end);

            History history = new History()
            {
                TotalIncome = income.Sum(x => x.Amount),
                Auto = expenses.Where(x => x.Category == "Auto").Sum(x => x.Amount),
                Clothing = expenses.Where(x => x.Category == "Clothing").Sum(x => x.Amount),
                HouseholdExpenses = expenses.Where(x => x.Category == "Household expenses").Sum(x => x.Amount),
                DebtPayments = expenses.Where(x => x.Category == "Debt Payments").Sum(x => x.Amount),
                Education = expenses.Where(x => x.Category == "Education").Sum(x => x.Amount),
                FoodExpenses = expenses.Where(x => x.Category == "Restaurant, Food, Groceries").Sum(x => x.Amount),
                InsuranceAndMedical = expenses.Where(x => x.Category == "Insurance and Medical").Sum(x => x.Amount),
                Investments = expenses.Where(x => x.Category == "Investments").Sum(x => x.Amount),
                MajorPurchases = expenses.Where(x => x.Category == "Major Purchases").Sum(x => x.Amount),
                NetCashFlow = income.Sum(x => x.Amount) - expenses.Sum(x => x.Amount),
                Other = expenses.Where(x => x.Category == "Other").Sum(x => x.Amount),
                Recreation = expenses.Where(x => x.Category == "Recreation").Sum(x => x.Amount),
                Taxes = expenses.Where(x => x.Category == "Taxes").Sum(x => x.Amount),
                TotalExpenses = expenses.Sum(x => x.Amount)
            };

            LogHelper.LogInfo("Exiting BR.GetHistory.");
            return history;
        }

        /// <summary>
        /// Gets a list of expense types. 
        /// </summary>
        /// <returns></returns>
        public List<ExpenseType> GetExpenseTypes()
        {
            LogHelper.LogInfo("Entering BR.GetExpenseTypes.");

            List<ExpenseType> expenseTypes = this.repository.GetExpenseTypes();

            LogHelper.LogInfo("Exiting BR.GetExpenseTypes.");
            return expenseTypes;
        }

        /// <summary>
        /// Gets a list of expense categories. 
        /// </summary>
        /// <returns> List of expense categories. </returns>
        public List<ExpenseCategory> GetExpenseCategories()
        {
            LogHelper.LogInfo("Entering BR.GetExpenseCategories.");

            List<ExpenseCategory> categories = this.repository.GetExpenseCategories();

            LogHelper.LogInfo("Exiting BR.GetExpenseCategories.");
            return categories;
        }

        /// <summary>
        /// Gets autocomplete suggestions for expense description. 
        /// </summary>
        /// <param name="expense"> Expense object. </param>
        /// <returns> Autocomplete suggestions. </returns>
        public string[] GetExpenseAutocomplete()
        {
            LogHelper.LogInfo("Entering BR.GetExpenseAutocomplete.");

            string[] suggestions = this.repository.GetExpenseAutocomplete();

            LogHelper.LogInfo("Exiting BR.GetExpenseAutocomplete.");

            return suggestions;
        }

        /// <summary>
        /// Gets autocomplete suggestions for the income source. 
        /// </summary>
        /// <param name="text"> Text to autocomplete. </param>
        /// <returns> Autocomplete suggestions. </returns>
        public string[] GetIncomeAutocomplete()
        {
            LogHelper.LogInfo("Entering BR.GetIncomeAutocomplete.");

            string[] suggestions = this.repository.GetIncomeAutocomplete();

            LogHelper.LogInfo("Exiting BR.GetIncomeAutocomplete.");

            return suggestions;
        }

        /// <summary>
        /// Queries the database for a list of expenses that match the given description. 
        /// </summary>
        /// <param name="start"> Start date. </param>
        /// <param name="end"> End date. </param>
        /// <param name="description"> Description. </param>
        /// <returns> A list of expenses. </returns>
        public List<Expense> QueryExpenses(DateTime start, DateTime end, string description)
        {
            LogHelper.LogInfo(string.Format("Entering BR.QueryExpenses with start = {0}, end = {1}, and description = {2}.", start.ToString(), end.ToString(), description));

            List<Expense> expenses = this.repository.QueryExpenses(start, end, description);

            expenses.Add(new Expense()
            {
                Amount = expenses.Sum(x => x.Amount),
                Description = "Total"
            });

            LogHelper.LogInfo("Exiting BR.QueryExpenses.");
            return expenses;
        }
    }
}
