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
        public QuotationDTO QuotationDTO { get; set; }

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

                    var result = _quotationService.CancelQuotationStatus(getQuotationId.Id, getQuotationId);
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
			ViewData["QuotationId1"] = quotationId;
			try
			{
				var getQuotationId = await _quotationService.GetQuotationById(quotationId);
				if (getQuotationId != null)
				{

					var result = _quotationService.UpdatesQuotationStatus(getQuotationId.Id, getQuotationId);
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
	}
}
