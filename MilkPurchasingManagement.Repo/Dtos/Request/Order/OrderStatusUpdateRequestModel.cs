using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkPurchasingManagement.Repo.Dtos.Request.Order
{
    public class OrderStatusUpdateRequestModel
    {
        public int OrderId { get; set; }
        public string NewStatus { get; set; }
    }
}
