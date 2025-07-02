namespace CSM.Web.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseSwaggerWithUi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
