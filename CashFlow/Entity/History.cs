using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashFlow.Entity
{
    public class History
    {
        public decimal? TotalIncome { get; set; }
        public decimal? DebtPayments { get; set; }
        public decimal? MajorPurchases { get; set; }
        public decimal? Recreation { get; set; }
        public decimal? HouseholdExpenses { get; set; }
        public decimal? FoodExpenses { get; set; }
        public decimal? InsuranceAndMedical { get; set; }
        public decimal? Auto { get; set; }
        public decimal? Clothing { get; set; }
        public decimal? Education { get; set; }
        public decimal? Taxes { get; set; }
        public decimal? Investments { get; set; }
        public decimal? Other { get; set; }
        public decimal? TotalExpenses { get; set; }
        public decimal? NetCashFlow { get; set; }
    }
}
