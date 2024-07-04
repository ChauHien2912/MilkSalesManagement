using MilkPurchasingManagement.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkPurchasingManagement.Repo.Dtos.Response.Order
{
    public class OrderResponseModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? PaymentId { get; set; }
        public string? DeliveryAdress { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<OrderDetailModel>? OrderDetails { get; set; } = new List<OrderDetailModel>();
        public class OrderDetailModel
        {
            public string? ProductName { get; set; }
            public string? Quantity { get; set; }
            public string? Price { get; set; }
        }
    }
}
