using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkPurchasingManagement.Repo.Dtos.Request.Order
{
    public class OrderDetailCreateRequestModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
