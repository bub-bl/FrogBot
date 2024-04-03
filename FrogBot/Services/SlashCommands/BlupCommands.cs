using Discord;
using Discord.Interactions;

namespace FrogBot.Services.SlashCommands;

[DefaultMemberPermissions(GuildPermission.ManageChannels)]
public sealed class BlupCommands : InteractionModuleBase<InteractionContext>
{
    [DefaultMemberPermissions(GuildPermission.Speak)]
    [SlashCommand("blup", "blup blup")]
    public async Task BlupCommand(string name)
    {
        await Context.Interaction.RespondAsync($"BLUP.. ? {name} !");
    }
}