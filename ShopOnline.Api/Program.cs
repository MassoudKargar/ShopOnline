var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ShopOnlineDbContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("ShopOnlineConnection")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy =>
    policy.WithOrigins("http://localhost:7257", "https://localhost:7257")
        .AllowAnyMethod()
        .WithHeaders(HeaderNames.ContentType)
);


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
