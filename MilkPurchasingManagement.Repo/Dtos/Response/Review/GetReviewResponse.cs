using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkPurchasingManagement.Repo.Dtos.Response.Review
{
    public class GetReviewResponse
    {
        public string? Content { get; set; }
        public int? Rate { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
    }
}
