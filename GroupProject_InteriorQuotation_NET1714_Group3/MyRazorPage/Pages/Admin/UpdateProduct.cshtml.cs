using Application.Interfaces;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;

namespace MyRazorPage.Pages.Admin
{
    public class UpdateProductModel : PageModel
    {
        private IProductService _productService;
        private IColorService _colorService; 
        private IMaterialService _materialService;
       
        public UpdateProductModel(IProductService productService, IColorService colorService, IMaterialService materialService)
        {
            _productService = productService;
            _colorService = colorService;
            _materialService = materialService;
                     
        }
      
        public ProductDto Product { get;  set; } 
        public List<ColorDTO> Color { get; set; }
        public List<MaterialDTO> Material { get; set; }
       
        public async Task<IActionResult> OnGet(int id)
        {
			Product = await   _productService.GetProductByIdWithAll(id);
            Color = await _colorService.GetAllColors();
            Material = await _materialService.GetAllMaterial();
            if (Product == null || Color == null || Material == null)
            {
                return NotFound();
            }
            ViewData["IsDeleted"] = Product.IsDeleted.ToString().ToLower();
            return Page();
        }
        public async Task<IActionResult> OnPost(bool isDeleted,string productName, string description, int quantity, int size, float price, int colorId, int materialId)
        {
            var id = Convert.ToInt32(Request.Form["id"]);
            var getpro = await _productService.GetProductByIdWithAll(id);
			if (getpro == null)
			{
				return NotFound();
			}           
            
            getpro.ProductName = productName;
            getpro.Description = description;
            getpro.Quantity = quantity;
            getpro.Size = size;
            getpro.Price = price;
            getpro.IsDeleted = isDeleted;          
            var colorName = await _colorService.GetColorNameById(colorId);
            if (colorName != null)
            {
                getpro.Color = colorName.ColourName;
            }           
            var materialName = await _materialService.GetMaterialById(materialId);
            if (materialName != null)
            {
                getpro.Material = materialName.MaterialName;
            }

            var result = await _productService.UpdateProduct(getpro);
			if (result == true)
			{
				return RedirectToPage("/Admin/ProductByAdmin");
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Failed to update product");
				return Page();
			}
		}        
    }
}
