using Microsoft.AspNetCore.Components;

using ShopOnline.App.Services.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.App.Pages;

public partial class ShoppingCart : ComponentBase
{
    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; }

    public List<CartItemDto>? ShoppingCartItems { get; set; }
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

    protected async Task DeleteCartItem_Click(int id)
    {
        var cartItemDto = await ShoppingCartService.DeleteItem(id);
        RemoveCartItem(id);
        //CartChanged();

    }

    private void RemoveCartItem(int id)
    {
        ShoppingCartItems?.Remove(GetCartItem(id));
    }

    private CartItemDto GetCartItem(int id)
    {
        return ShoppingCartItems.FirstOrDefault(i => i.Id == id);
    }
}