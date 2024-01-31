using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Weather.Services.AuthApi.Data;
using Weather.Services.AuthApi.Extensioins;
using Weather.Services.AuthApi.Extensions.Middlewares;
using Weather.Services.AuthApi.Models;
using Weather.Services.AuthApi.Services;
using Weather.Services.AuthApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

#region Business Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
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

#region DbContext
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
#endregion

#region AutoMapper
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

#region SwaggerAuth
builder.AddAppAuthentication();
#endregion

builder.Services.AddControllers();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
