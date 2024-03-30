using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace MyRazorPage.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountService _account;
        public RegisterModel(IAccountService account)
        {
            _account = account;
        }

        public void OnGet()
        {
        }

        public async Task OnPost(string fullname, string Address, string phone, string email, string password, string cfpassword) 
        {

            string selectedGender = Request.Form["Gender"];
            ViewData["fullnamedata"] = fullname;
            ViewData["Addressdata"] = Address;
            ViewData["phonedata"] = phone;
            ViewData["emaildata"] = email;
            ViewData["passworddata"] = password;
            ViewData["cfpassworddata"] = cfpassword;
            ViewData["genderdata"] = selectedGender;
            if ( await Validate( fullname, Address, phone, email, password, cfpassword, selectedGender) == true)
            {
                
                    AccountDTO account = new AccountDTO();
                    account.FullName = fullname;
                    account.Address = Address;
                    account.TelephoneNumber = phone;
                    account.Password = password;
                    account.Gender = selectedGender;
                    account.EmailAddress = email;
                    account.RoleName = "Customer";
                    account.Status = 1;
                    bool registed = await _account.Register(account);
                    if (registed == true)
                    {
                        ViewData["msgRegister"] = "You are registed new account successfully!";
                    }
                    else
                    {
                        ViewData["msgRegister"] = "Registed account failed";
                    }
                
            }
            OnGet();
        }    
        private async Task<bool> Validate(string fullname, string address, string telephone, string email, string password, string cfpassword, string gender)
        {
            bool flag = true;
            if (string.IsNullOrEmpty(fullname))
            {
                ViewData["fullname"] = "Please fill fullname";
                flag = false;
            }
            if (!string.IsNullOrEmpty(fullname))
            {
                if(fullname.Length < 5)
                {
                    ViewData["fullname"] = "Your name's length should be more 5 letters";
                    flag = false;
                } 
            }
            if (string.IsNullOrEmpty(address))
            {
                ViewData["address"] = "Please fill address";
                flag = false;
            }
            if (string.IsNullOrEmpty(telephone))
            {
                
                ViewData["telephone"] = "Please fill telephone";
                flag = false;
            }
            if (!string.IsNullOrEmpty(telephone))
            {
                if(!telephone.StartsWith("0"))
                {
                    ViewData["telephone"] = "Your phone number should be start with 0";
                    flag = false;
                }
                if(!telephone.Length.Equals(10))
                {
                    ViewData["telephone"] = "Your phoneNumber should be equal to 10 numbers";
                    flag = false;
                }
            }
            if (string.IsNullOrEmpty(email))
            {
                ViewData["email"] = "Please fill email";
                flag = false;
            }
            if (string.IsNullOrEmpty(password))
            {
                ViewData["password"] = "Please fill password";
                flag = false;
            }
            if (string.IsNullOrEmpty(cfpassword))
            {
                ViewData["cfpassword"] = "Please fill cfpassword";
                flag = false;
            }
            if (string.IsNullOrEmpty(gender))
            {
                ViewData["gender"] = "Please choose gender";
                flag = false;
            }
            if (!string.IsNullOrEmpty(cfpassword) && !string.IsNullOrEmpty(password)) 
            {
                if (!cfpassword.Equals(password))
                {
                    ViewData["cfpasswordnotsame"] = "Password must equal confirm password";
                    flag = false;
                }
            }
            if(!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(email))
            {
                bool r = await _account.CheckEmailAddressExisted(email);
                if (r == true)
                {
                    ViewData["email"] = "Email is existed";
                    flag = false;
                }
            }
           return flag;
        }
    }
}
