using Application.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDTO> Login(AccountLoginDTO account);
        Task<bool> CheckEmailAddressExisted(string emailaddress);
        Task<bool> CheckPhoneNumberExited(string phonenumber);
        Task<bool> Register(AccountDTO account);
        Task<List<AccountDTO>> GetAccounts();
        Task<List<AccountDTO>> GetAccountByName(string name);
        Task<AccountDTO> GetAccountByID(int id);
        Task<bool> DeleteAccount(AccountDTO account);
        Task<bool> UpdateAccount(AccountDTO account);
    }
}
