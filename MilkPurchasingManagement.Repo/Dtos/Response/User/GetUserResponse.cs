using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkPurchasingManagement.Repo.Dtos.Response.User
{
    public class GetUserResponse
    {
        public int? Roleid { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public decimal? Phone { get; set; }
        public string? Address { get; set; }
    }
}
