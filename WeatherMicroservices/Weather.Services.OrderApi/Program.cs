using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Weather.Services.CartApi.Data;
using Weather.Services.CartApi.Extensioins;
using Weather.Services.CartApi.Extensions.Middlewares;
using Weather.Services.CartApi.Services;
using Weather.Services.CartApi.Services.Interfaces;
using Weather.Services.CartApi.Utilities.ClientHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

#region Handlers with clients
builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("Product", u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"]!))
    .AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
#endregion

#region DbContext
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region Business Services
builder.Services.AddScoped<ICartService,CartService>();
#endregion

#region Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "frontend",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200", "http://127.0.0.1:4200")
                   .AllowAnyHeader()
                   .WithExposedHeaders("x-total-numbers-of-games")
                   .AllowAnyMethod();
        });
});
#endregion

#region AutoMapper
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

#region SwaggerAuth
builder.AddAppAuthentication();
#endregion

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Cors
app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());
#endregion  
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
