using Microsoft.EntityFrameworkCore;
using NutritionAPI.Data;
using NutritionAPI.Helper;
using NutritionAPI.Interfaces;
using NutritionAPI.Repository;
using NutritionAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddScoped<IFoodItemRepository, FoodItemsRepository>();
builder.Services.AddScoped<IMappingService, MappingService>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();