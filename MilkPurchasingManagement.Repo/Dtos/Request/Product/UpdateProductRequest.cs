using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkPurchasingManagement.Repo.Dtos.Request.Product
{
    public class UpdateProductRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? Brand { get; set; }
        public string? ImgUrl { get; set; }
        public decimal? Volume { get; set; }
        public int? AgeAllowed { get; set; }
    }
}
