using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using FrogBot.Services.Hosted;
using FrogBot.Services.Hosted.Interactions;
using BotService = FrogBot.Services.Hosted.Entry.BotService;

namespace FrogBot.Extensions;

public static class FrogBotExtensions
{
    public static IServiceCollection AddFrogBot(this IServiceCollection services)
    {
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged,
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