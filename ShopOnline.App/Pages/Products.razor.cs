using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components;
using ShopOnline.App.Services.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.App.Pages;

public partial class Products : ComponentBase
{
    [Inject]
    public IProductService ProductService { get; set; }

    public IEnumerable<ProductDto>? ProductDtos { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ProductDtos = await ProductService.GetItems();
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