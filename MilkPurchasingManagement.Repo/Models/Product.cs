using System;
using System.Collections.Generic;

namespace MilkPurchasingManagement.Repo.Models
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            Images = new HashSet<Image>();
            OrderDetails = new HashSet<OrderDetail>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? Brand { get; set; }
        public decimal? Volume { get; set; }
        public int? AgeAllowed { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
