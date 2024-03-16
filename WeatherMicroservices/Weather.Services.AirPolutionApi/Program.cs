using Weather.Services.AirPolutionApi.Extensions.Services;
using Weather.Services.AirPolutionApi.Services;
using Weather.Services.AirPolutionApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
#region Handlers with clients
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("Air", u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:AirApi"]!));
#endregion

#region Business Services
builder.Services.AddScoped<IAirService, AirService>();
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

#region SwaggerAuth
builder.AddAppAuthentication();
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
