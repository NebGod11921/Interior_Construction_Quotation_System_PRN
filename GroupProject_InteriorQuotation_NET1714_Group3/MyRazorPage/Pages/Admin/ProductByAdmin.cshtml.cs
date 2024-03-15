using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyRazorPage.Pages.Admin
{
    public class ProductByAdminModel : PageModel
    {
        private IProductService _productService;
        public ProductByAdminModel(IProductService product)
        {
            _productService = product;
        }
        public string searchBar { get; set; } 
        public List<ProductDto> productDtos { get; set; }
        public void OnGet(string search)
        {
            searchBar = search;
            if(!string.IsNullOrEmpty(searchBar))
            {
                productDtos = _productService.GetAllProductSearch(searchBar);
            }
            else
            {
                productDtos = _productService.GetAllProduct().ToList();
            }     
        }
        
    }
}
