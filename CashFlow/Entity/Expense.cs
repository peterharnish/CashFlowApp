using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashFlow.Entity
{
    public class Expense
    {
        public int ID { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public int ExpenseTypeID { get; set; }
        public int ExpenseCategoryID { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ExpenseType { get; set; }
    }
}
