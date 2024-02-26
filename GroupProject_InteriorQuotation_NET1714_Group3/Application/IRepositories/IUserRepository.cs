using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByEmailAddressAndPasswordHash(string Email, string passwordHash);
        Task<bool> CheckEmailAddressExisted(string emailaddress);
        Task<bool> CheckPhoneNumberExited(string phonenumber);
        Task<IEnumerable<User>> SearchAccountByNameAsync(string name);
        Task<IEnumerable<User>> SearchAccountByRoleNameAsync(string roleName);
        Task<IEnumerable<User>> GetSortedAccountAsync();
        bool Register(User user);
        public bool Delete(int id);
    }
}
