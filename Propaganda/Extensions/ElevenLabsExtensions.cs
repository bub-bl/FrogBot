using ElevenLabs;

namespace Propaganda.Extensions;

public static class ElevenLabsExtensions
{
    public static IServiceCollection ConfigureElevenLabs(this IServiceCollection services)
    {
        var client = new ElevenLabsClient(ElevenLabsAuthentication.LoadFromEnv());
        services.AddSingleton(client);

        return services;
    }
}