using Discord;

namespace Propaganda.Services.Hosted.Entry;

public sealed partial class BotService
{
    private async Task ClientOnReady()
    {
        try
        {
#if DEBUG
            if (!ulong.TryParse(configuration["Bot:GuildId"], out var guildId))
                throw new Exception("No guild id provided");

            await interaction.RegisterCommandsToGuildAsync(guildId);
#else
            await interaction.RegisterCommandsGloballyAsync();
#endif

            logger.LogInformation("Commands registered");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
        }
    }

    private Task ClientOnLog(LogMessage arg)
    {
        logger.LogInformation(arg.Message);
        return Task.CompletedTask;
    }
}