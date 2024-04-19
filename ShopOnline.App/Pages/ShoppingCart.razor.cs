using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using ShopOnline.App.Services.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.App.Pages;

public partial class ShoppingCart : ComponentBase
{
    [Inject]
    public IJSRuntime Js { get; set; }

    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; }

    public List<CartItemDto>? ShoppingCartItems { get; set; }
    public string? ErrorMessage { get; set; }

    protected string TotalPrice { get; set; }
    protected int TotalQuantity { get; set; }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
            CalculateCartSummaryTotals();
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }

    protected async Task UpdateQtyCartItem_Click(int id, int qty)
    {
        try
        {
            if (qty > 0)
            {
                var updateItemDto = new CartItemQtyUpdateDto()
                {
                    CartItemId = id,
                    Qty = qty
                };
                var returnedUpdateItemDto = await ShoppingCartService.UpdateQty(updateItemDto);
                UpdateItemTotalPrice(returnedUpdateItemDto);
                CalculateCartSummaryTotals();
                await MakeUpdateQtyButtonVisible(id, false);
            }
            else
            {
                var item = ShoppingCartItems.FirstOrDefault(i => i.Id == id);
                if (item != null)
                {
                    item.Qty = 1;
                    item.TotalPrice = item.Price;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void UpdateItemTotalPrice(CartItemDto cartItemDto)
    {
        var item = GetCartItem(cartItemDto.Id);
        if (item != null)
        {
            item.TotalPrice = cartItemDto.Price * cartItemDto.Qty;
        }
    }
    private void CalculateCartSummaryTotals()
    {
        SetTotalPrice();
        SetTotalQuantity();
    }
    private void SetTotalPrice()
    {
        TotalPrice = ShoppingCartItems.Sum(p => p.TotalPrice).ToString("C");
    }

    private void SetTotalQuantity()
    {
        TotalQuantity = ShoppingCartItems.Sum(p => p.Qty);
    }
    protected async Task DeleteCartItem_Click(int id)
    {
        var cartItemDto = await ShoppingCartService.DeleteItem(id);
        RemoveCartItem(id);
        CalculateCartSummaryTotals();
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


    protected async Task UpdateQty_Input(int id)
    {
        await MakeUpdateQtyButtonVisible(id, true);
    }

    private async Task MakeUpdateQtyButtonVisible(int id, bool visible)
    {
        await Js.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, visible);
    }
}