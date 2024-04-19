using ShopOnline.Models.Dtos;

namespace ShopOnline.App.Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetItems();
    Task<ProductDto?> GetItem(int id);
    Task<IEnumerable<ProductCategoryDto>> GetProductCategories();
    Task<IEnumerable<ProductDto>> GetItemsByCategory(int categoryId);
}