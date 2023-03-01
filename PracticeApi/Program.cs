using log4net.Core;
using TravelInsuranceAPI.Controllers;
using TravelInsuranceAPI.Services.IRepository;
using TravelInsuranceAPI.Services.Repository;
using TravelInsuranceAPI.Utils.JWTMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITravelRepository, TravelRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

//builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
//var app = builder.Build();

// Configure the HTTP request pipeline.


var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();

    }
    app.UseAuthorization();
    app.UseHttpsRedirection();

    app.UseMiddleware<JwtMiddleware>();

    

   

    ControllerActionEndpointConventionBuilder controllerActionEndpointConventionBuilder = app.MapControllers();

    app.Run();
}


