using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CashFlow.Entity;

namespace CashFlow.DataAccess
{
    public interface IRepository
    {
        List<Expense> GetExpenses(DateTime start, DateTime end);
        List<Income> GetIncome(DateTime start, DateTime end);
        int InsertExpense(Expense expense);
        int InsertIncome(Income income);
        List<ExpenseType> GetExpenseTypes();
        List<ExpenseCategory> GetExpenseCategories();
        string[] GetExpenseAutocomplete();
        string[] GetIncomeAutocomplete();
        List<Expense> QueryExpenses(DateTime start, DateTime end, string description);
    }
}
