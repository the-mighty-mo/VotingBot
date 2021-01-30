using Discord;
using Discord.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using static VotingBot.DatabaseManager;

namespace VotingBot.Modules
{
    public class ClearVotes : ModuleBase<SocketCommandContext>
    {
        [Command("clearvotes")]
        [Alias("clear-votes")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task ClearVotesAsync()
        {
            List<Task> cmds = new List<Task>
            {
                votesDatabase.Votes.RemoveVotesAsync(Context.Guild)
            };

            EmbedBuilder embed = new EmbedBuilder()
                .WithColor(SecurityInfo.botColor)
                .WithDescription("All votes have been cleared from the database.");
            cmds.Add(Context.Channel.SendMessageAsync(embed: embed.Build()));

            await Task.WhenAll(cmds);
        }
    }
}
