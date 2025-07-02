using CSM.Web.Infrastructure;

namespace CSM.Web;

public static class DependencyInjection
{
    public static void AddWeb(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
    }
}