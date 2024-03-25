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
		private readonly IRoomService _roomService;
        public QuotationHistoryModel(IQuotationService quotationService, IRoomService roomService)
        {
            _quotationService = quotationService;
			_roomService = roomService;
        }
        public IEnumerable<QuotationDTO> Quotations { get; set; }
		public IEnumerable<RoomDTOS> RoomDTOs { get; set; }

		/* public void OnGet()
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

		 }*/
		public async Task OnGet()
		{
			var csSessionValue = HttpContext.Session.GetString("csSession");
			if (csSessionValue != null)
			{
				var myObject = JsonSerializer.Deserialize<AccountDTO>(csSessionValue);
				Quotations = await  _quotationService.GetQuotationByCsId(myObject.Id);
				RoomDTOs = await _roomService.GetAllRooms();
			} else
			{
				Page();
			}
				
			
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
	}
}
