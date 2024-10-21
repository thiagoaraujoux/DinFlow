using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DinFlow.Models
{
    public class DashboardViewModel
    {
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal TotalEconomias { get; set; }
    }
}