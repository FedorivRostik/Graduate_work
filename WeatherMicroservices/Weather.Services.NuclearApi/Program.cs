using AutoMapper;
using Weather.Services.NuclearApi.Extensioins;
using Weather.Services.NuclearApi.Services;
using Weather.Services.NuclearApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
#region Business Services
builder.Services.AddScoped<INuclearService, NuclearService>();
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
