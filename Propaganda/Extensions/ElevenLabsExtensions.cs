using ElevenLabs;

namespace Propaganda.Extensions;

public static class ElevenLabsExtensions
{
    public static IServiceCollection ConfigureElevenLabs(this IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();
        var configuration = provider.GetRequiredService<IConfiguration>();
        var apiKey = configuration["ELEVEN_LABS_API_KEY"];
        
        var auth = new ElevenLabsAuthentication(apiKey);
        var client = new ElevenLabsClient(auth);
        services.AddSingleton(client);

        return services;
    }
}