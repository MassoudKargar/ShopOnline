namespace ShopOnline.Api.Extensions;

public static class DtoConversions
{
    public static IEnumerable<ProductCategoryDto> ConvertToDto(this IEnumerable<ProductCategory> productCategories)
    {
        return (productCategories.Select(productCategory => new ProductCategoryDto
        {
            Id = productCategory.Id, Name = productCategory.Name, IconCSS = productCategory.IconCSS
        })).ToList();
    }
    public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products)
    {
        return (products.Select(product => new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            ImageURL = product.ImageURL,
            Price = product.Price,
            Qty = product.Qty,
            CategoryId = product.ProductCategory.Id,
            CategoryName = product.ProductCategory.Name
        })).ToList();
        
    }
    public static ProductDto ConvertToDto(this Product product)
                                                   
    {
        return new ProductDto
        {
            Id=product.Id,
            Name = product.Name,
            Description = product.Description,
            ImageURL = product.ImageURL,
            Price = product.Price,
            Qty = product.Qty,
            CategoryId = product.ProductCategory.Id,
            CategoryName = product.ProductCategory.Name

        };

    }

    public static IEnumerable<CartItemDto> ConvertToDto(this IEnumerable<CartItem> cartItems,
        IEnumerable<Product> products)
    {
        return (cartItems.Join(products, cartItem => cartItem.ProductId, product => product.Id,
            (cartItem, product) => new CartItemDto
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageURL = product.ImageURL,
                Price = product.Price,
                CartId = cartItem.CartId,
                Qty = cartItem.Qty,
                TotalPrice = product.Price * cartItem.Qty
            })).ToList();
    }
    public static CartItemDto ConvertToDto(this CartItem cartItem,
        Product product)
    {
        return new CartItemDto
        {
            Id = cartItem.Id,
            ProductId = cartItem.ProductId,
            ProductName = product.Name,
            ProductDescription = product.Description,
            ProductImageURL = product.ImageURL,
            Price = product.Price,
            CartId = cartItem.CartId,
            Qty = cartItem.Qty,
            TotalPrice = product.Price * cartItem.Qty
        };
    }

}