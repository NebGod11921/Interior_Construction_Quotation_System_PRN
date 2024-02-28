using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace MyRazorPage.Pages
{
	public class RoomDetailModel : PageModel
	{
		private readonly IProductService _productService;

		public RoomDetailModel(IProductService productService)
		{
			_productService = productService;
		}

		public List<ProductDto> Products { get; private set; }

		public IActionResult OnGet(/*[FromQuery] */int roomId)
		{
			Products = _productService.GetAllProductByRoomId(roomId);
			
			if (Products == null || Products.Count == 0)
			{
				return NotFound();
			}
			return Page();
		}
	}
}
