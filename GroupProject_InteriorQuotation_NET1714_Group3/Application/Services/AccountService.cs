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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteAccount(AccountDTO account)
        {
            try
            {
                var userexited = await _unit.UserRepository.GetByIdAsync(account.Id);

                if (userexited != null)
                {
                    userexited.Status = 0; // Đánh dấu tài khoản là đã xóa

                    //_unit.UserRepository.SoftRemove(userexited); // Xóa mềm tài khoản

                    await _unit.SaveChangeAsync(); // Lưu thay đổi

                    return true;
                }
                else
                {
                    return false; // Không tìm thấy tài khoản cần xóa
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AccountDTO> GetAccountByID(int id)
        {
            try
            {
                var user = (User?) await _unit.UserRepository.GetByIdAsync(id);
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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message );
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
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Register(AccountDTO account)
        {
            try
            {
                User user_mapper = _mapper.Map<User>(account);
                bool registed = await _unit.UserRepository.Register(user_mapper);
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
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAccount(AccountDTO account)
        {
            try
            {
                var userexited = await _unit.UserRepository.GetByIdAsync(account.Id);

                if (userexited != null)
                {
                    userexited.FullName = account.FullName;
                    userexited.EmailAddress = account.EmailAddress;
                    userexited.Password = account.Password;
                    userexited.Address = account.Address;
                    userexited.TelephoneNumber = account.TelephoneNumber;
                    userexited.Status = account.Status;
                    userexited.Gender = account.Gender;
                    userexited.RoleName = account.RoleName;
                    await _unit.SaveChangeAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
