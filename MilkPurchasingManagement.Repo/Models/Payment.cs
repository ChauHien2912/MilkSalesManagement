using System;
using System.Collections.Generic;

namespace MilkPurchasingManagement.Repo.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string? PaymentMethodName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
