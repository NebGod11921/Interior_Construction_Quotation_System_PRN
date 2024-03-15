using Application.Interfaces;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Reflection;
using System.Text.Json;

namespace MyRazorPage.Pages
{
    public class RoomManagementModel : PageModel
    {
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;

        public RoomManagementModel(IRoomService roomService, IRoomTypeService roomTypeService)
        {
            _roomService = roomService;
            _roomTypeService = roomTypeService;

        }
        public IEnumerable<RoomDTOS> roomDTOs { get; set; }
        public IEnumerable<RoomTypeDTOS> roomTypeDTOs { get; set; }
        

        public async Task OnGet()
        {
            roomDTOs = await _roomService.GetAllRoom();
            roomTypeDTOs = await _roomTypeService.GetAllTypesRoom();
            
        }
        
        public async Task OnPostAddNewRoom(float area, string roomdescription, DateTime creationDate, int RoomType, bool IsDeleted)
        {
            ViewData["Areadata"] = area;
            ViewData["RoomDescriptiondata"] = roomdescription;
            ViewData["CreationDatedata"] = creationDate;
            ViewData["RoomTypedata"] = RoomType;
            ViewData["Statusdata"] = IsDeleted;


            try
            {
                RoomDTOS roomDTOS = new RoomDTOS();
                bool v = CheckValidate(area,roomdescription,creationDate, RoomType, IsDeleted);
                if (v)
                {
                    
                    roomDTOS.Area = area;
                    roomDTOS.RoomDescription = roomdescription;
                    roomDTOS.CreationDate = creationDate;
                    roomDTOS.RoomTypeId = RoomType;
                    roomDTOS.IsDeleted = IsDeleted;
                    var result = await _roomService.CreateRoom(roomDTOS);
                    if (result != null)
                    {
                        RedirectToPage("/RoomManagement");
                    }
                    else
                    {
                        Page();
                    }
                }
                Page();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            await OnGet();
        }

        private bool CheckValidate(float area, string roomdescription, DateTime creationDate, int roomTypeId, bool IsDeleted)
        {
            bool flag = true;
            if (string.IsNullOrEmpty(roomdescription) || string.IsNullOrWhiteSpace(roomdescription))
            {
                ViewData["msgRoomDescription"] = "Please fill in fulldescription , this not white space";
                flag = false;
            }
            if (area == null)
            {
                ViewData["msgArea"] = "Please fill in area , this not white space";
                flag = false;
            }
            if (creationDate == null)
            {
                ViewData["msgArea"] = "Please fill in DateTime , this not white space";
                flag = false;
            }
            if (creationDate != null)
            {
                if (creationDate < DateTime.UtcNow)
                {
                    ViewData["msgCreationDate"] = "Please choose the present date";
                    flag = false;
                }
                
            }

            if (area != null)
            {
                if (area < 0)
                {
                    ViewData["msgArea"] = "Area must be greater than 0";
                    flag = false;
                }
            }
            if (IsDeleted.Equals(""))
            {
                ViewData["msgStatus"] = "Please checked your Status";
                flag = false;
            }
            if (roomTypeId <= 0 || roomTypeId.Equals(""))
            {
                ViewData["msgRoomType"] = "Please choose your roomType";
                flag = false;
            }
            return flag;
        }
    }
        
}
