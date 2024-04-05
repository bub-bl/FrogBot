using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace Propaganda.Services.Hosted.Entry;

public sealed partial class BotService(
    DiscordSocketClient client,
    InteractionService interaction,
    IConfiguration configuration,
    ILogger<BotService> logger) : IHostedService, IDisposable
{
    private Thread _thread = null!;
    private string _token = null!;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _token = configuration["Bot:Token"] ?? string.Empty;

        if (string.IsNullOrEmpty(_token))
            throw new Exception("No token provided");

        _thread = new Thread(Start);
        _thread.Start();

        return Task.CompletedTask;

        async void Start()
        {
            client.Ready += ClientOnReady;
            client.Log += ClientOnLog;

            await client.LoginAsync(TokenType.Bot, _token);
            await client.StartAsync();

            // Bloque la tâche actuelle jusquà ce que le programme ce ferme
            await Task.Delay(-1, cancellationToken);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        client.Ready -= ClientOnReady;
        client.Log -= ClientOnLog;

        await client.StopAsync();
    }

    public void Dispose()
    {
        client.Dispose();
        interaction.Dispose();
    }
}