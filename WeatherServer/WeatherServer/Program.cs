using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using WeatherServer.Api.Controllers;
using WeatherServer.Application;
using WeatherServer.Core.Entities;
using WeatherServer.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
	.AddApplicationPart(typeof(AuthController).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Name = "Authorization",
		Description = "Bearer Authentication with JWT Token",
		Type = SecuritySchemeType.Http
	});
	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
		  new OpenApiSecurityScheme
		  {
			  Reference= new OpenApiReference
			  {
				  Id="Bearer",
				  Type=ReferenceType.SecurityScheme
			  }
		  },
		  new List<string>()
		}
	});
});

//database
builder.Services.AddDbContext<WeatherDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//Add Identity
var tokenValidationParameters = new TokenValidationParameters()
{
	ValidateIssuerSigningKey = true,
	IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:Secret"])),

	ValidateIssuer = true,
	ValidIssuer = builder.Configuration["JWT:Issuer"],

	ValidateAudience = true,
	ValidAudience = builder.Configuration["JWT:Audience"],

	ValidateLifetime = true,
	ClockSkew = TimeSpan.Zero,

};
builder.Services.AddSingleton(tokenValidationParameters);

builder.Services.AddIdentity<WeatherUser, IdentityRole>()
	.AddEntityFrameworkStores<WeatherDbContext>()
	.AddDefaultTokenProviders();

//Add Authentication
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
//Add JWT Bearer
.AddJwtBearer(options =>
{
	options.SaveToken = true;
	options.RequireHttpsMetadata = false;
	options.TokenValidationParameters = tokenValidationParameters;
});

//services
builder.Services.AddApplicationServices();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();