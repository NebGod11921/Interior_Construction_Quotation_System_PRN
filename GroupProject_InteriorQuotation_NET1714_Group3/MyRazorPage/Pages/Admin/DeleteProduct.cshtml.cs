using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyRazorPage.Pages.Admin
{
    public class DeleteProductModel : PageModel
    {
        private IProductService _productService;
        private IColorService _colorService;
        private IMaterialService _materialService;

        public DeleteProductModel(IProductService productService, IColorService colorService, IMaterialService materialService)
        {
            _productService = productService;
            _colorService = colorService;
            _materialService = materialService;

        }

        public ProductDto Product { get; set; }
        public List<ColorDTO> Color { get; set; }
        public List<MaterialDTO> Material { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            Product = await _productService.GetProductByIdWithAll(id);
            Color = await _colorService.GetAllColors();
            Material = await _materialService.GetAllMaterial();
            if (Product == null || Color == null || Material == null)
            {
                return NotFound();
            }

            return Page();
        }
        public async Task<IActionResult> OnPost(int id)
        {            
            bool isDeleted = await _productService.DeleteProduct(id);
            if (isDeleted)
            {
                return RedirectToPage("/Admin/ProductByAdmin");
            }
            else
            {               
                return NotFound();
            }
        }
    }
}
