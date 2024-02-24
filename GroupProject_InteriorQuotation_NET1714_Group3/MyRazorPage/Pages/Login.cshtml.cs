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

        public IActionResult OnPostLogin(string email, string password)
        {
            validatetion(email, password);
            AccountLoginDTO accountLoginDTO = new AccountLoginDTO();
            accountLoginDTO.EmailAddress = email;
            accountLoginDTO.Password = password;
            var login =  _account.Login(accountLoginDTO);
            if(login != null)
            {
                ViewData["msgLogin"] = "Cant found account in system.Please checked again";
            }
            else
            {
                RedirectToPage("/Home");
            }
            OnGet();
            return Page();
        }

        public void validatetion(string email, string password)
        {
            if(email.Length == 0)
            {
                ViewData["email"] = "Please fill email, this cant null";
            }else if( password.Length == 0)
            {
                ViewData["password"] = "Please fill password, this cant null";
            }
            OnGet();
        }
    }
}
