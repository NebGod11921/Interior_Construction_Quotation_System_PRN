using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace MyRazorPage.Pages.Shared
{
    public class QuotationManagementModel : PageModel
    {
        private readonly IQuotationService _quotationService;
        public IEnumerable<QuotationDTO> Quotations { get; set; }
        

        public QuotationManagementModel(IQuotationService quotationService)
        {
            _quotationService = quotationService;
            
        }

        public async Task OnGet()
        {
            Quotations = await _quotationService.GetAllQuotation();
        }


        public async Task OnPostCancel(int quotationId)
        {
            ViewData["QuotationId"] = quotationId;
            try
            {
                var getQuotationId = await _quotationService.GetQuotationById(quotationId);
                if (getQuotationId != null)
                {

                    var result = await _quotationService.CancelQuotationStatus(getQuotationId.Id, getQuotationId);
                    if (result != null)
                    {
                        RedirectToPage("/QuotationManagement");                     
                    } else
                    {
                        Page();
                    }
                } else
                {
                    Page();
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            await OnGet();
        }
		public async Task OnPostReConfirm(int quotationId)
		{
			ViewData["QuotationId"] = quotationId;
			try
			{
				var getQuotationId = await _quotationService.GetQuotationById(quotationId);
				if (getQuotationId != null)
				{

					var result = await _quotationService.UpdatesQuotationStatus(getQuotationId.Id, getQuotationId);
					if (result != null)
					{
						RedirectToPage("/QuotationManagement");
					}
					else
					{
						Page();
					}
				}
				else
				{
					Page();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			await OnGet();

		}
        public async Task OnPostTransferUpdate(int quotationId)
        {
            ViewData["QuotationId"] = quotationId;

            try
            {
                var getQuotationById = await _quotationService.GetQuotationById(quotationId);
                if (getQuotationById != null)
                {
                    var json = JsonSerializer.Serialize(getQuotationById);
                    HttpContext.Session.SetString("quotationById", json);
                    RedirectToPage("/UpdateQuotation");
                }
                else
                {
                    Page();
                }


            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            await OnGet();

        }

	}
}
