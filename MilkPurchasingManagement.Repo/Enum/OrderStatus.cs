using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkPurchasingManagement.Repo.Enum
{
    public class OrderStatus
    {
        public enum OrdersStatus
        {
            Not_yet_delivered,
            Delivering,
            Successfully_delivered
        }
    }
}
