using Application.IRepositories;
using Domain.Entities;
using Infrustructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
            
        public Task<bool> CheckEmailAddressExisted(string emailaddress)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckPhoneNumberExited(string phonenumber)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetSortedAccountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmailAddressAndPasswordHash(string username, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> SearchAccountByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> SearchAccountByRoleNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
