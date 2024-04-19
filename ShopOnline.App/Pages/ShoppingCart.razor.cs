using Microsoft.AspNetCore.Components;

using ShopOnline.App.Services.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.App.Pages;

public partial class ShoppingCart : ComponentBase
{
    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; }

    public IEnumerable<CartItemDto>? ShoppingCartItems { get; set; }
    public string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {

            ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }
}