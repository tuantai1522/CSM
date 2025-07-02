using System.Reflection;
using CSM.Infrastructure;
using CSM.UseCases;
using CSM.Web;
using CSM.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGenWithAuth();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddWeb();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.MapEndpoints();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
}

// Use global custom exception handler
app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();


app.Run();