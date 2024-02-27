using Application.IRepositories;
using Domain.Entities;
using Infrustructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Domain.Entities;
using Infrustructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _db = appDbContext;
        }
            
        public async Task<bool> CheckEmailAddressExisted(string emailaddress)
        {
            try
            {
                var users = await _db.Users.Where(x => x.EmailAddress.ToLower().Equals(emailaddress.ToLower())).FirstOrDefaultAsync();
                if (users != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.InnerException.ToString());
            }
        }

        public async Task<bool> CheckPhoneNumberExited(string phonenumber)
        {
            try
            {
                var users = await _db.Users.FirstOrDefaultAsync(x => x.TelephoneNumber.Equals(phonenumber));
                if (users != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.ToString());
            }
        }

        public async Task<IEnumerable<User>> GetSortedAccountAsync()
        {
            try
            {
                var users = await _db.Users.OrderByDescending(x => x.FullName).ToListAsync();
                if (users != null)
                {
                    return users;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.ToString());
            }
        }

        public async Task<User> GetUserByEmailAddressAndPasswordHash(string Email, string passwordHash)
        {
            try
            {
                var users = await _db.Users.FirstOrDefaultAsync(x => x.EmailAddress.ToLower().Equals(Email.ToLower()) && x.Password.Equals(passwordHash));
                if (users != null)
                {
                    return users;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.ToString());
            }
        }

        public async Task<bool> Register(User user)
        {
            
                await _db.AddAsync(user);
                if(await SaveChangeAsync()> 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            
        }
        public async Task<bool> Delete(User user)
        {
            bool flag = true;
                var exits = await _db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
                if (exits != null)
                {
                     _db.Update(exits);
                    if (await _db.SaveChangesAsync() > 0)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
            return flag;
        }

        // Method to save changes
        private async Task<int> SaveChangeAsync()
        {
            return await _db.SaveChangesAsync();
        }
        public async Task<IEnumerable<User>> SearchAccountByNameAsync(string name)
        {
            try
            {
                var users = await _db.Users.Where(x => x.FullName.ToLower().Equals(name.ToLower())).ToListAsync();
                if (users.Count > 0)
                {
                    return users;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.ToString());
            }
        }

        public async Task<IEnumerable<User>> SearchAccountByRoleNameAsync(string roleName)
        {
            try
            {
                var users = await _db.Users.Where(x => x.RoleName.ToLower().Equals(roleName.ToLower())).ToListAsync();
                if(users.Count > 0)
                {
                    return users;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.ToString());
            }
        }
    }
}
