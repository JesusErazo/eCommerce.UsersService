using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Add infrastructure services.
builder.Services.AddInfrastructure();

//Add core services.
builder.Services.AddCore();

//Add controllers to the service collection.
builder.Services.AddControllers().AddJsonOptions(options =>
{
  options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

//Build the web application
var app = builder.Build();

//Global exception handler
app.UseExceptionHandlingMiddleware();

//Routing
app.UseRouting();

//Auth
app.UseAuthentication();
app.UseAuthorization();

//Controller routes
app.MapControllers();
app.Run();
