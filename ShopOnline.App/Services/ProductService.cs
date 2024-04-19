using ShopOnline.App.Services.Contracts;
using ShopOnline.Models.Dtos;
using System.Net.Http.Json;

namespace ShopOnline.App.Services;

public class ProductService(HttpClient httpClient) : IProductService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<ProductDto?> GetItem(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Product/{id}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<ProductDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status code: {response.StatusCode} message: {message}");
            }
        }
        catch (Exception)
        {
            //Log exception
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> GetItems()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Product");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<ProductDto>();
                }

                return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>() ?? Array.Empty<ProductDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status code: {response.StatusCode} message: {message}");
            }

        }
        catch (Exception)
        {
            //Log exception
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> GetItemsByCategory(int categoryId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Product/{categoryId}/GetItemsByCategory");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<ProductDto>();
                }
                return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
            }
        }
        catch (Exception)
        {
            //Log exception
            throw;
        }
    }

    public async Task<IEnumerable<ProductCategoryDto>> GetProductCategories()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Product/GetProductCategories");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<ProductCategoryDto>();
                }
                return await response.Content.ReadFromJsonAsync<IEnumerable<ProductCategoryDto>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
            }
        }
        catch (Exception)
        {
            //Log exception
            throw;
        }
    }
}
