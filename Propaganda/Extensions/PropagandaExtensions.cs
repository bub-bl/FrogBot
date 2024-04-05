using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Propaganda.Services.Hosted.Interactions;
using BotService = Propaganda.Services.Hosted.Entry.BotService;

namespace Propaganda.Extensions;

public static class PropagandaExtensions
{
    public static IServiceCollection AddPropaganda(this IServiceCollection services)
    {
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.All,
            LogLevel = LogSeverity.Info,
            MessageCacheSize = 0
        };

        var client = new DiscordSocketClient(config);

        services.AddSingleton(new InteractionService(client, new InteractionServiceConfig
        {
            DefaultRunMode = RunMode.Async,
            LogLevel = LogSeverity.Debug,
            ThrowOnError = true
        }));

        services.AddSingleton(client);
        services.AddHostedService<BotService>();
        services.AddHostedService<Interaction>();

        return services;
    }
}