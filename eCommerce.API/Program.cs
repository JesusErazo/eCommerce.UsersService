using eCommerce.Core;
using eCommerce.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

var builder = WebApplication.CreateBuilder(args);

//Add infrastructure services.
builder.Services.AddInfrastructure();

//Add core services.
builder.Services.AddCore();

//Add controllers to the service collection.
builder.Services.AddControllers();

//Build the web application
var app = builder.Build();

//Routing
app.UseRouting();

//Auth
app.UseAuthentication();
app.UseAuthorization();

//Controller routes
app.MapControllers();
app.Run();
