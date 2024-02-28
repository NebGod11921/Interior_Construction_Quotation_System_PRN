using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyRazorPage.Pages
{
    public class TestModel : PageModel
    {
        private readonly ILogger<TestModel> _logger;
        private readonly IRoomTypeService _roomTypeService;
        public List<RoomHomePageTitle> ProductsHomePage { get; set; }
        public TestModel(ILogger<TestModel> logger, IRoomTypeService roomService)
        {
            _logger = logger;
            _roomTypeService = roomService;
        }

        public void OnGet()
        {
            var check= _roomTypeService.GetAllRoomTypes();
            if (check == null)
            {
                Console.WriteLine("Not Found");
            }
            else
            {
                ProductsHomePage = _roomTypeService.GetAllRoomTypes();
            }
        }
        public string GetRoomTypeImageUrl(string roomType, string imageName)
        {
            return $"/Images/{roomType}/{Path.GetFileName(imageName)}";
        }

    }
}
