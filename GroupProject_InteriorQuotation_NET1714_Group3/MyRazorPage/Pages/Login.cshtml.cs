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
        private readonly IConfiguration _config;
        public LoginModel(IAccountService account, IConfiguration config)
        {
            _account = account;
            _config = config;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string email, string password)
        {
            if(validatetion(email, password) == true)
            {
                if (CheckAdmin(email, password))
                {
                    return RedirectToPage("/Admin");
                }
                else
                {
                    AccountLoginDTO accountLoginDTO = new AccountLoginDTO();
                    accountLoginDTO.EmailAddress = email;
                    accountLoginDTO.Password = password;
                    bool c = await _account.CheckEmailAddressExisted(email);
                    AccountDTO login = await _account.Login(accountLoginDTO);
                    if (login == null)
                    {

                        ViewData["msgLogin"] = "Cant found account in system.Please checked again";
                    }
                    else
                    {
                        return RedirectToPage("/Index");
                    }
                }
            }
            OnGet();
            return Page();
        }

        public bool validatetion(string email, string password)
        {
            bool flag = true;
            if(string.IsNullOrEmpty(email))
            {
                ViewData["email"] = "Please fill email, this cant null";
                 flag = false;
            }
            if(string.IsNullOrEmpty(password))
            {
                ViewData["password"] = "Please fill password, this cant null";
                flag = false;
            }
            return flag;
        }
        private bool CheckAdmin(string name, string password)
        {
            IConfiguration config = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                       .AddJsonFile("appsettings.json", true, true)
                                       .Build();
            var aEmail = _config["Admin:UserName"];
            var aPass = _config["Admin:Password"];

            return name.ToLower() == aEmail.ToLower() && password.ToLower() == aPass.ToLower();
        }
    }
}
