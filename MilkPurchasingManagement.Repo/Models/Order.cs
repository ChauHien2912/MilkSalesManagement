using System;
using System.Collections.Generic;

namespace MilkPurchasingManagement.Repo.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? PaymentId { get; set; }
        public string? DeliveryAdress { get; set; }
        public string? Status { get; set; }

        public virtual Payment? Payment { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
