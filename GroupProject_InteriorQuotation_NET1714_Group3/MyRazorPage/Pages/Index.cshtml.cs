using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyRazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IRoomTypeService _roomTypeService;
        public List<RoomHomePageProduct> ProductsHomePage { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IRoomTypeService roomService)
        {
            _logger = logger;
            _roomTypeService = roomService;
        }

        public void OnGet()
        {
            ProductsHomePage = _roomTypeService.GetAllRoomTypesWithProducts();
        }
        public string GetRoomTypeImageUrl(string roomType, string imageName)
        {
            return $"/images/{roomType}/{Path.GetFileName(imageName)}";
        }

    }
}
