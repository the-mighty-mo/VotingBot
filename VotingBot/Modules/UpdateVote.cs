using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using static VotingBot.DatabaseManager;

namespace VotingBot.Modules
{
    public class UpdateVote : ModuleBase<SocketCommandContext>
    {
        [Command("updatevote")]
        [Alias("update-vote")]
        public async Task UpdateVoteAsync(int voteId, [Remainder] string message)
        {
            await votesDatabase.Votes.UpdateVoteMessageAsync(Context.Guild, voteId, message);

            EmbedBuilder embed = new EmbedBuilder()
                .WithColor(SecurityInfo.botColor)
                .WithDescription($"The message for Vote {voteId} has been updated.");
            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}
