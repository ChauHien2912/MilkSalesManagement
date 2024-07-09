using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkPurchasingManagement.Repo.Dtos.Request.Order
{
    public class CreateOrderRequest
    {
        public int? UserId { get; set; }
      
        
        public int? PaymentId { get; set; }
        public string? DeliveryAdress { get; set; }
        public string? Status { get; set; }
        public List<OrderDetailCreateRequestModel> OrderDetails { get; set; }
    }
}
