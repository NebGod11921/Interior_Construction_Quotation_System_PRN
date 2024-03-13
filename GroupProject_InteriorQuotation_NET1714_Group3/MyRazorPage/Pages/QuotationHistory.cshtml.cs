using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace MyRazorPage.Pages
{
    public class QuotationHistoryModel : PageModel
    {
        private readonly IQuotationService _quotationService;

        public QuotationHistoryModel(IQuotationService quotationService)
        {
            _quotationService = quotationService;
        }
        public List<QuotationDTO> Quotations;
        

        public void OnGet()
        {
            var csSessionValue = HttpContext.Session.GetString("csSession");
            if (csSessionValue != null)
            {
                var myObject = JsonSerializer.Deserialize<AccountDTO>(csSessionValue);
                Quotations = _quotationService.GetQuotationByCsId(myObject.Id);
            }
            else
            {
                Quotations = new List<QuotationDTO>();
            }

        }
    }
}
