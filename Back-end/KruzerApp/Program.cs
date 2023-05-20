using KruzerApp.Models;
using KruzerApp.Repositories;
using KruzerApp.Repositories.impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<KruzerContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("KruzerAppDb")));

builder.Services.AddCors(c =>
{
    c.AddPolicy("CorsPolicy", options => options.AllowCredentials().AllowAnyMethod().AllowAnyHeader()
           .WithOrigins("http://localhost:4200", "http://localhost:3000"));
});



// Add services to the container.
builder.Services.AddTransient<IPutnikRepository, PutnikRepository>();
builder.Services.AddTransient<IKrstarenjeRepository, KrstarenjeRepository>();
builder.Services.AddTransient<IRezervacijaRepository, RezervacijaRepository>();
builder.Services.AddTransient<ILokacijaRepository, LokacijaRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
