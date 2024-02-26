using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public AccountService(IUnitOfWork unit, IMapper mapper) 
        { 
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<bool> CheckEmailAddressExisted(string emailaddress)
        {
            try
            {
                bool user = await _unit.UserRepository.CheckEmailAddressExisted(emailaddress);
                if(user == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
        public async Task<bool> CheckPhoneNumberExited(string phonenumber)
        {
            try
            {
                bool user = await _unit.UserRepository.CheckPhoneNumberExited(phonenumber);
                if (user == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
        public async Task<bool> DeleteAccount(int id)
        {
            try
            {
                if (_unit.UserRepository.Delete(id) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
        public async Task<AccountDTO> GetAccountByID(int id)
        {
            try
            {
                var user = (User?)await _unit.UserRepository.GetByIdAsync(id);
                if (user != null)
                {
                    AccountDTO accountDTO = _mapper.Map<AccountDTO>(user);
                    return accountDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
        public async Task<List<AccountDTO>> GetAccounts()
        {
            try
            {
                var user =(List<User>) await _unit.UserRepository.GetSortedAccountAsync();
                if (user != null)
                {
                    List<AccountDTO> accountDTO = _mapper.Map<List<AccountDTO>>(user);
                    return accountDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
        public  async Task<AccountDTO> Login(AccountLoginDTO account)
        {
            try
            {
                var user = await _unit.UserRepository.GetUserByEmailAddressAndPasswordHash(account.EmailAddress, account.Password);
                if (user != null)
                {
                    AccountDTO accountDTO = _mapper.Map<AccountDTO>(user);
                    return accountDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
        public async Task<bool> Register(AccountDTO account)
        {
            try
            {
                User user_mapper = _mapper.Map<User>(account);
                bool registed = _unit.UserRepository.Register(user_mapper);
                if (registed == false) 
                {
                    return false;
                }
                else
                {
                    return true;
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}
