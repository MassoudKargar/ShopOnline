using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;

namespace ShopOnline.App.Pages;

public partial class DisplayProducts : ComponentBase
{
    [Parameter]
    public IEnumerable<ProductDto> ProductDtos { get; set; }
}