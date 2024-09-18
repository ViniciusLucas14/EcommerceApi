using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;

namespace Ecommerce.Data
{
    public class Startup
    {
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("sti3").AddResilienceHandler("my-pipeline", builder =>
            {
                builder.AddRetry(new HttpRetryStrategyOptions
                {
                    MaxRetryAttempts=5,
                    Delay = TimeSpan.FromSeconds(3),
                });
                builder.AddTimeout(TimeSpan.FromSeconds(5));
            });

        }
    }
}
