using Discord.Interactions;
using Discord.WebSocket;

namespace FrogBot.Services.Hosted.Interactions;

public sealed partial class Interaction
{
    private async Task OnInteractionCreated(SocketInteraction socketInteraction)
    {
        if (socketInteraction.User.IsBot) return;
        
        try
        {
            var ctx = new InteractionContext(client, socketInteraction);
            await interaction.ExecuteCommandAsync(ctx, services);
        }
        catch(Exception ex)
        {
            logger.LogError(ex.ToString());
        }
    }
}