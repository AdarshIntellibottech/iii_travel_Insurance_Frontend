using log4net.Core;
using TravelInsuranceAPI.Controllers;
using TravelInsuranceAPI.Services.Repositories;
using TravelInsuranceAPI.Services.IRepositories;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITravelRepository, TravelRepository>();


//builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

ControllerActionEndpointConventionBuilder controllerActionEndpointConventionBuilder = app.MapControllers();

app.Run();


