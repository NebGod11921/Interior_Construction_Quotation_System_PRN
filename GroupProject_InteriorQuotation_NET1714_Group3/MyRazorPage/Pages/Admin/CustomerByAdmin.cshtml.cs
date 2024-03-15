using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyRazorPage.Pages.Admin
{
    public class CustomerByAdminModel : PageModel
    {
        private readonly IAccountService _account;
        //private readonly 
        public CustomerByAdminModel(IAccountService account)
        {
            _account = account;
        }
        public List<AccountDTO> accountDTOs;
        public List<AccountDTO> accountDTOTop4;

        public async Task OnGet()
        {
            accountDTOs = await _account.GetAccounts() ?? new List<AccountDTO>();
            accountDTOTop4 = accountDTOs.OrderByDescending(x => x.Id).Take(4).ToList() ?? new List<AccountDTO>();
        }
        public async Task<IActionResult> OnPostSearch(string searchinput)
        {
            if (searchinput == null)
            {
                ViewData["msgSearch"] = "Please fill input for result you wan to it.";
            }
            else
            {
                accountDTOs = await _account.GetAccountByName(searchinput);
                accountDTOTop4 = accountDTOs.OrderByDescending(x => x.Id).Take(4).ToList() ?? new List<AccountDTO>();
            }
            return Page();
        }
        public async Task OnPostDeleteCustomer(int csID)
        {
            var aexits = await _account.GetAccountByID(csID);
            if (aexits.Status == 0)
            {
                ViewData["msgdelete"] = "Account has deleted, Cant delete again.";
            }
            else
            {

                var deleted = await _account.DeleteAccount(aexits);

                if (deleted == true)
                {
                    ViewData["msgdelete"] = "Account has be deleted successfully";


                }
                else
                {
                    ViewData["msgdelete"] = "Account has be deleted fail";

                }

            }
            await OnGet();

        }
        public async Task OnPostAddNewCustomer(string fullName, string telephone, string address, string email, string password, string cfpassword)
        {
            string gender = Request.Form["gender"];

            string role = Request.Form["role"];

            TempData["PopupState"] = "open";

            ViewData["fullnamedata"] = fullName;
            ViewData["telephonedata"] = telephone;
            ViewData["addressdata"] = address;
            ViewData["emaildata"] = email;
            ViewData["passworddata"] = password;
            ViewData["cfpassworddata"] = cfpassword;
            ViewData["genderdata"] = gender;
            ViewData["roledata"] = role;
            bool v = checkValidate(fullName, email, address, password, cfpassword, telephone, gender, role);
            if (v)
            {
                AccountDTO account = new AccountDTO();
                account.FullName = fullName;
                account.EmailAddress = email;
                account.Password = password;
                account.TelephoneNumber = telephone;
                account.Address = address;
                account.Gender = gender;
                account.RoleName = role;
                account.Status = 1;
                bool added = await _account.Register(account);
                if (added == true)
                {
                    ViewData["AddNewCS"] = "You are add new account successfully";
                }
                else
                {
                    ViewData["AddNewCS"] = "Add New Account Fail";
                }
            }
            await OnGet();
        }

        private bool checkValidate(string fullname, string email, string address, string pass, string cfpass, string telephone, string gender, string rolename)
        {
            bool flag = true;
            if (string.IsNullOrEmpty(fullname) || string.IsNullOrWhiteSpace(fullname))
            {
                ViewData["msgfullname"] = "Please fill in full name , this not white space";
                flag = false;
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                ViewData["msgemail"] = "Please fill in email , this not white space";
                flag = false;
            }
            if (string.IsNullOrWhiteSpace(address))
            {
                ViewData["msgaddress"] = "Please fill in address , this not white space";
                flag = false;
            }
            if (string.IsNullOrWhiteSpace(pass))
            {
                ViewData["msgpassword"] = "Please fill in pass , this not white space";
                flag = false;
            }
            if (string.IsNullOrWhiteSpace(cfpass))
            {
                ViewData["msgcfpassword"] = "Please fill in confirm pass , this not white space";
                flag = false;
            }
            if (!string.IsNullOrWhiteSpace(pass) && !string.IsNullOrWhiteSpace(cfpass))
            {
                if (!cfpass.Equals(pass))
                {
                    ViewData["msgcfpassword"] = "Confirm password must same password";
                    flag = false;
                }
            }
            if (string.IsNullOrWhiteSpace(telephone))
            {
                ViewData["msgtelephone"] = "Please fill in telephone , this not white space";
                flag = false;
            }
            if (!string.IsNullOrWhiteSpace(telephone))
            {
                if (!telephone.StartsWith('0') || telephone.Length != 10)
                {
                    ViewData["msgtelephone"] = "Telephone must start with number 0, and have 10 number";
                    flag = false;
                }
            }
            if (string.IsNullOrEmpty(gender))
            {
                ViewData["msggender"] = "PlePlease checked gender";
                flag = false;
            }
            if (string.IsNullOrEmpty(rolename))
            {
                ViewData["msgrole"] = "PlePlease choose role";
                flag = false;
            }
            return flag;
        }

        public async Task OnPostUpdateAccount(int csID, string fullName, string telephone, string address, string email, string password, string cfpassword)
        {
            TempData["PopupUpdateState"] = "open";
            TempData["csID"] = csID;
            string gender = Request.Form["gender"];

            string role = Request.Form["role"];

            byte status = Convert.ToByte(Request.Form["status"]);
            ViewData["fullnamedata"] = fullName;
            ViewData["telephonedata"] = telephone;
            ViewData["addressdata"] = address;
            ViewData["emaildata"] = email;
            ViewData["passworddata"] = password;
            ViewData["cfpassworddata"] = cfpassword;
            ViewData["genderdata"] = gender;
            ViewData["roledata"] = role;
            ViewData["statusdata"] = status;
            bool v = checkValidate(fullName, email, address, password, cfpassword, telephone, gender, role);
            if (v)
            {
                AccountDTO account = new AccountDTO();
                account.Id = csID;
                account.FullName = fullName;
                account.EmailAddress = email;
                account.Password = password;
                account.TelephoneNumber = telephone;
                account.Address = address;
                account.Gender = gender;
                account.RoleName = role;
                account.Status = status;
                var updated = await _account.UpdateAccount(account);

                if (updated == true)
                {
                    ViewData["msgupdate"] = "Account has be updated successfully";
                    TempData["PopupUpdateState"] = "open";
                }
                else
                {
                    ViewData["msgupdate"] = "Account has be update fail";
                }

            }
            await OnGet();
        }
    }
}
