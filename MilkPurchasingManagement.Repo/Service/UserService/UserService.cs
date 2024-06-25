using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MilkPurchasingManagement.Repo.Dtos.Response.User;
using MilkPurchasingManagement.Repo.Models;
using MilkPurchasingManagement.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkPurchasingManagement.Repo.Service.UserService
{
    public class UserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
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

    }
}
