using CSM.Infrastructure;
using CSM.UseCases;
using CSM.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddWeb();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Use global custom exception handler
app.UseExceptionHandler();


app.Run();