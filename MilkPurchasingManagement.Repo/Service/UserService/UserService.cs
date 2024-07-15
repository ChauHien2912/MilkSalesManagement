using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MilkPurchasingManagement.Repo.Dtos.Request.User;
using MilkPurchasingManagement.Repo.Dtos.Response;
using MilkPurchasingManagement.Repo.Dtos.Response.User;
using MilkPurchasingManagement.Repo.Models;
using MilkPurchasingManagement.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MilkPurchasingManagement.Repo.Service.UserService
{
    public class UserService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper, IConfiguration config)
        {
            _uow = uow;
            _mapper = mapper;
            _config = config;
        }

        public async Task<GetUserResponse> GetUserByID(int id)
        {
            var userexisting = await _uow.GetRepository<User>().SingleOrDefaultAsync(predicate: e=> e.Id == id,
                include: source => source.Include(a => a.Role)
                );
            if(userexisting == null)
            {
                throw new Exception("User Not found !!!!");
            }
            return _mapper.Map<GetUserResponse>(userexisting);
        }

        public async Task<ApiResponse> Login(LoginModel login)
        {
            var user = await _uow.GetRepository<User>().SingleOrDefaultAsync(predicate: m => m.Email.Equals(login.Email));
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Invalid email or password"
                };
            }

            return new ApiResponse
            {
                Success = true,
                Message = "Login successfully",
                Data = GenerateToken(user)
            };
        }

        public async Task<ApiResponse> Register(SignUpModel registerModel)
        {
            var userCheck = await _uow.GetRepository<User>().SingleOrDefaultAsync(predicate: m => m.Email.Equals(registerModel.Email));
            if (userCheck != null)
            {
                return new ApiResponse { Success = false, Message = "Email already exists" };
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerModel.Password);

            var newUser = new User
            {
                Username = registerModel.Username,
                Email = registerModel.Email,
                Password = hashedPassword,
                Address = registerModel.Location,
                FullName = registerModel.Fullname,
                
                
                Phone = registerModel.PhoneNumber,
                Roleid = registerModel.RoleId,
               
            };

           _uow.GetRepository<User>().InsertAsync(newUser);
            _uow.Commit();

            return new ApiResponse { Success = true, Message = "Registration successful" };
        }

        public ApiResponse Logout()
        {
            // Invalidate token (implement chosen approach here)
            return new ApiResponse { Success = true, Message = "Logged out successfully" };
        }
        public async Task<ApiResponse> ChangePassword(ChangePasswordModel model)
        {
            var user = await _uow.GetRepository<User>().SingleOrDefaultAsync(predicate: u => u.Id == model.UserId);
            if (user == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            if (!BCrypt.Net.BCrypt.Verify(model.CurrentPassword, user.Password))
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Current password is incorrect"
                };
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            _uow.GetRepository<User>().UpdateAsync(user);
            _uow.Commit();

            return new ApiResponse
            {
                Success = true,
                Message = "Password changed successfully"
            };
        }
        public string GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key cannot be null"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim("unique_name", user.FullName ?? throw new InvalidOperationException("Fullname cannot be null")),
            new Claim("id", user.Id.ToString() ?? throw new InvalidOperationException("Fullname cannot be null")),
            new Claim(ClaimTypes.Role, user.Roleid.ToString()),
            new Claim(ClaimTypes.Email, user.Email ?? throw new InvalidOperationException("Email cannot be null")),
            new Claim("phone_number", user.Phone.ToString() ?? throw new InvalidOperationException("Phone cannot be null")),
            new Claim("address", user.Address) ?? throw new InvalidOperationException("Address cannot be null"),
            new Claim("TokenId", Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

    }
}
