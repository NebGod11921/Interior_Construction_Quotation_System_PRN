using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyRazorPage.Pages.Shared
{
    public class QuotationManagementModel : PageModel
    {
        private readonly IQuotationService _quotationService;
        public List<QuotationDTO> Quotations { get; set; }

        public QuotationManagementModel(IQuotationService quotationService)
        {
            _quotationService = quotationService;
            
        }

        public async Task OnGet()
        {
            Quotations = await _quotationService.GetAllQuotation();
        }
    }
}
