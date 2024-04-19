using System.Runtime.InteropServices;

using Microsoft.AspNetCore.Components;

using ShopOnline.App.Services.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.App.Pages;

public partial class Products : ComponentBase
{
    [Inject]
    public IProductService ProductService { get; set; }

    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; }

    public IEnumerable<ProductDto>? ProductDtos { get; set; }
    public string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            ProductDtos = await ProductService.GetItems();
            var shoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
            var totalQty = shoppingCartItems.Sum(i => i.Qty);
            ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQty);
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }

    protected IOrderedEnumerable<IGrouping<int, ProductDto>>? GetGroupedProductsByCategory()
    {
        if (ProductDtos != null)
            return ProductDtos.GroupBy(product => product.CategoryId).OrderBy(prodByCatGroup => prodByCatGroup.Key);

        return null;
    }
    protected string? GetCategoryName(IGrouping<int, ProductDto> groupedProductDtos)
    {
        return groupedProductDtos.FirstOrDefault(pg => pg.CategoryId == groupedProductDtos.Key)?.CategoryName;
    }
}