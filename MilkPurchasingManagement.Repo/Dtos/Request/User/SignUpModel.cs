using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkPurchasingManagement.Repo.Dtos.Request.User
{
    public class SignUpModel
    {

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Location { get; set; }

        public string Email { get; set; } = null!;

        public string? Fullname { get; set; }

       

     

        public decimal? PhoneNumber { get; set; }
        public int? RoleId { get; set; }
    }
}
