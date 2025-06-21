using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Core.Mappers;
using eCommerce.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Add infrastructure services.
builder.Services.AddInfrastructure();

//Add core services.
builder.Services.AddCore();

//Add controllers to the service collection.
builder.Services.AddControllers()
 .AddJsonOptions(options =>
 {
   options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
 });

builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);

//Add API explorer services
builder.Services.AddEndpointsApiExplorer();

//Add swagger generation services to create swagger specification
builder.Services.AddSwaggerGen();

//Add CORS services
builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(builder =>
  {
    builder.WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader();
  });
});

//Build the web application
var app = builder.Build();

//Global exception handler
app.UseExceptionHandlingMiddleware();

//Routing
app.UseRouting();

//Adds endpoint  that can serve the swagger.json file
app.UseSwagger();

//Adds swagger UI (interactive page to explore and test API endpoints)
app.UseSwaggerUI();

//CORS
app.UseCors();

//Auth
app.UseAuthentication();
app.UseAuthorization();

//Controller routes
app.MapControllers();
app.Run();
