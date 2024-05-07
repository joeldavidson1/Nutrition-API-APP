using AutoMapper;
using Microsoft.AspNetCore.HttpOverrides;
using Npgsql;
using NutritionAPI.Extensions;
using Service;
using Service.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Retrieve connection string and password from local environment variables
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRINGS");
var password = Environment.GetEnvironmentVariable("PASSWORD");

// Create a NpgsqlConnectionStringBuilder and set the connection string and password
var conStrBuilder = new NpgsqlConnectionStringBuilder(connectionString)
{
    Password = password
};

// Get the connection string
var connection = conStrBuilder.ConnectionString;;

// Add services to the container.
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.ConfigureCors();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(connection);
builder.Services.ConfigureSwagger();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IAuthenticationManager, AuthenticationManager>();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(NutritionAPI.Presentation.AssemblyReference).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger(); 
app.UseSwaggerUI(sw =>
    sw.SwaggerEndpoint("/swagger/v1/swagger.json", "Nutrition API"));
app.UseDeveloperExceptionPage();

// else
    // app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();