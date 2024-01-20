using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Weather.Services.ProductApi.Data;
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

#region Business Services
builder.Services.AddScoped<IProductService, ProductService>();
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

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
