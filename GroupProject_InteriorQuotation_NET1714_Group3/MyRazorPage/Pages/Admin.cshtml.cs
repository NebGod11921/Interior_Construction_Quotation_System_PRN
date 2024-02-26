using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyRazorPage.Pages
{
    public class AdminModel : PageModel
    {
        private readonly IAccountService _account;
        public AdminModel(IAccountService account)
        {
            _account = account;
        }
        public List<AccountDTO> accountDTOs;
        public async Task OnGet()
        {
            accountDTOs = await _account.GetAccounts() ?? new List<AccountDTO>();
        }
    }
}
