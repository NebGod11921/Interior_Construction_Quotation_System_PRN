using Application.Interfaces;
using Application.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace MyRazorPage.Pages
{
    public class UpdateQuotationModel : PageModel
    {
        private readonly IQuotationService _quotationService;

        public UpdateQuotationModel(IQuotationService quotationService)
        {
            _quotationService = quotationService;
        }
        public IEnumerable<QuotationDTO> Quotations { get; set; }

        public async Task OnGet()
        {


            Quotations = await _quotationService.GetAllQuotation();
            
          
        }
    }
}
