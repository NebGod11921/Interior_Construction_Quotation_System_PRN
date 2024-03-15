using Application.Interfaces;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
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
        
    }
        
}
