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
        Task<IEnumerable<User>> SearchAccountByFullNameAsync(string name);
        Task<IEnumerable<User>> SearchAccountByRoleNameAsync(string roleName);
        Task<IEnumerable<User>> GetSortedAccountAsync();
        Task<bool> Register(User user);
        Task<bool> Delete(User user);
    }
}
