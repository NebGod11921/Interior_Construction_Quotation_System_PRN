using Application.Interfaces;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyRazorPage.Pages
{
    public class AllProductModel : PageModel
    {
        private IProductService _productService;
        public AllProductModel(IProductService productService)
        {
            _productService = productService;
        }
        public string SearchProduct { get; set; }
        public List<ProductDto> GetProductList {  get; set; }
        public void OnGet(string search)
        {
            SearchProduct = search;
            if(!string.IsNullOrEmpty(SearchProduct))
            {
                GetProductList = _productService.GetAllProductSearch(SearchProduct);
            }
            else
            {
                GetProductList = _productService.GetAllProduct();   
            }
        }
    }
}
