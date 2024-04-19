using Microsoft.AspNetCore.Components;
using ShopOnline.App.Services.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.App.Pages;

public partial class ProductDetails : ComponentBase
{
    [Parameter]
    public int Id { get; set; }

    [Inject]
    public IProductService ProductService { get; set; }

    public ProductDto? Product { get; set; }
    public string? ErrorMessage { get; set; }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            Product = await ProductService.GetItem(id: Id);
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }
}