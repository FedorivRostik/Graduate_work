using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Weather.Services.ProductApi.Data;
using Weather.Services.ProductApi.Data.Repositories;
using Weather.Services.ProductApi.Data.Repositories.Interfaces;
using Weather.Services.ProductApi.Extensioins;
using Weather.Services.ProductApi.Extensions.Middlewares;
using Weather.Services.ProductApi.Services;
using Weather.Services.ProductApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

#region DbContext
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region Data
builder.Services.AddScoped<IPictureRepository, PictureRepository>();
#endregion

#region Azure
builder.Services.AddScoped(_ => new BlobServiceClient(builder.Configuration.GetConnectionString("BlobConnection")));
#endregion

#region Business Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IGenreService, GenreService>();
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
