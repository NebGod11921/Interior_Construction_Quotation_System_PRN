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
		public async Task OnPostPlaceOrder(int quotationId)
		{
			ViewData["QuotationId"] = quotationId;
			try
			{
				var getQuotationId = await _quotationService.GetQuotationById(quotationId);
				if (getQuotationId != null)
				{

					var result = await _quotationService.SuccessfulQuotationStatus(getQuotationId.Id, getQuotationId);
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
        public async Task OnPostUpdateQuotation(int qID, string quotationName, float unitPrice, float totalPrice, DateTime createDate, int rID)
        {
            TempData["PopupUpdateState"] = "open";
            TempData["qID"] = qID;
			TempData["rID"] = rID;
            ViewData["quotationnamedata"] = quotationName;
            ViewData["unitpricedata"] = unitPrice;
            ViewData["totalpricedata"] = totalPrice;
            ViewData["createdatedata"] = createDate;
			bool val = Validation(quotationName, unitPrice, totalPrice, createDate);
			if (val)
			{
				QuotationDTO quotationDTO = new QuotationDTO();
				quotationDTO.QuotationName = quotationName;
				quotationDTO.UnitPrice = unitPrice;
				quotationDTO.TotalPrice = totalPrice;
				quotationDTO.RoomId = rID;
				var result = await _quotationService.UpdateQuotation(quotationDTO, qID);
				if (result == true)
				{
					ViewData["msgupdate"] = "Quotation's details has be updated successfully";
					TempData["PopupUpdateState"] = "open";
				} else
				{
					ViewData["msgupdate"] = "Quotation's details has be update fail";
				}
			}
			await OnGet();
		}

        private bool Validation(string quotationName, float unitPrice, float totalPrice, DateTime createDate)
        {
            bool flag = true;
			if (string.IsNullOrEmpty(quotationName) || string.IsNullOrWhiteSpace(quotationName))
			{
				ViewData["msgfullname"] = "Please fill in quotation name , this not white space";
				flag = false;
			}
			if (unitPrice < 0 || unitPrice.Equals(""))
			{
				ViewData["msgfullname"] = "Please fill in your unit price, price must not be empty";
				flag = false;
			}
			if (totalPrice < 0 || totalPrice.Equals(""))
			{
				ViewData["msgfullname"] = "Please fill in your total price , price must not be empty";
				flag = false;
			}
			if (createDate > DateTime.UtcNow || createDate.Equals(""))
			{
				ViewData["msgfullname"] = "Please fill in createDate , your createDate must be in the present";
				flag = false;
			}
			return flag;
		}
    }
}
