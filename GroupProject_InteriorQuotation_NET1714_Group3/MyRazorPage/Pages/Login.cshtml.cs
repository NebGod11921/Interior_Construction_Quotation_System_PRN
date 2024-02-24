using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Numerics;

namespace MyRazorPage.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _account;
        public LoginModel(IAccountService account)
        {
            _account = account;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string email, string password)
        {
            validatetion(email, password);
            AccountLoginDTO accountLoginDTO = new AccountLoginDTO();
            accountLoginDTO.EmailAddress = email;
            accountLoginDTO.Password = password;
            bool c = await _account.CheckEmailAddressExisted(email);
            var login =  _account.Login(accountLoginDTO);
            if(login == null)
            {
                ViewData["msgLogin"] = "Cant found account in system.Please checked again";
            }
            else
            {
                return RedirectToPage("/Home");
            }
            OnGet();
            return Page();
        }

        public void validatetion(string email, string password)
        {
            if(string.IsNullOrEmpty(email))
            {
                ViewData["email"] = "Please fill email, this cant null";
            }else if(string.IsNullOrEmpty(password))
            {
                ViewData["password"] = "Please fill password, this cant null";
            }
        }
    }
}
