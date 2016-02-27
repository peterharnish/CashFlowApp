using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashFlow.Entity
{
    public class Income
    {
        public int ID { get; set; }
        public decimal? Amount { get; set; }
        public string Source { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
