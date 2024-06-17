using Microsoft.EntityFrameworkCore;
using MilkPurchasingManagement.Repo.Helper;
using MilkPurchasingManagement.Repo.Models;
using WareHouseManagement.API.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Dependency Injection
builder.Services.AddDbContext<MilkSalesManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddUnitOfWork();
builder.Services.AddDIServices();
builder.Services.AddDIRepositories();
builder.Services.AddDIAccessor();


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
