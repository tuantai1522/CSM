namespace CSM.Web.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseSwaggerWithUi(this WebApplication app)
    {
        app.UseSwagger();
        
        // To retrieve jwt token if user exists browser
        app.UseSwaggerUI(c =>
        {
            c.ConfigObject.PersistAuthorization = true;
        });
    }
}
