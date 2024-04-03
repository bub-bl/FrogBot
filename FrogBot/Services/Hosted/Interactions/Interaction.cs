using System.Reflection;
using Discord.Interactions;
using Discord.WebSocket;

namespace FrogBot.Services.Hosted.Interactions;

public sealed partial class Interaction(
    DiscordSocketClient client,
    InteractionService interaction,
    IServiceProvider services,
    ILogger<Interaction> logger)
    : IHostedService, IDisposable
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        client.InteractionCreated += OnInteractionCreated;
        await interaction.AddModulesAsync(Assembly.GetEntryAssembly(), services);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        client.InteractionCreated -= OnInteractionCreated;
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        client.Dispose();
        interaction.Dispose();
    }
}