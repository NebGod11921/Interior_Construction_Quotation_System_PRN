using Application.Interfaces;
using Application.ViewModels;
using Domain.Entities;
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
        /*public async Task OnPostTransferUpdate(int quotationId)
        {
			ViewData["QuotationId"] = quotationId;
			try
            {
				var httpGet = HttpContext.Session.GetString("quotationById");
                if (httpGet != null)
                {
                    var json = JsonSerializer.Deserialize<QuotationDTO>(httpGet);
					var getQuotationById = await _quotationService.GetQuotationById(json.Id);
					if (getQuotationById != null && quotationId == getQuotationById.Id)
					{
						var result = JsonSerializer.Serialize(getQuotationById);
						HttpContext.Session.SetString("qqq", result);
						RedirectToPage("/UpdateQuotation");
					}
					else
					{
						Page();
					}
				}

				


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            await OnGet();
        }*/

    }
}
