using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyRazorPage.Pages
{
    public class AdminModel : PageModel
    {
        private readonly IAccountService _account;
        //private readonly 
        public AdminModel(IAccountService account)
        {
            _account = account;
        }
        public List<AccountDTO> accountDTOs;
        public async Task OnGet()
        {
            accountDTOs = await _account.GetAccounts() ?? new List<AccountDTO>();
        }

        public async void OnPostDeleteCustomer(int csID)
        {
            AccountDTO aexits = await _account.GetAccountByID(csID);
            if (aexits.Status == 0)
            {
                ViewData["msgdelete"] = "Account has deleted, Cant delete again.";
            }
            else
            {

                bool deleted = await _account.DeleteAccount(csID);
                if (deleted == true) 
                {
                    ViewData["msgdelete"] = "Account has be deleted successfully";
                }
                else
                {
                    ViewData["msgdelete"] = "Account has be deleted fail";
                }
            }
            OnGet();
        }
    }
}
