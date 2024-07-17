using AuthenticationService.Extensions;


var builder = WebApplication.CreateBuilder(args);
builder.AddDatabase();
builder.AddServices();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();