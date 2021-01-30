using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using static VotingBot.DatabaseManager;

namespace VotingBot.Modules
{
    public class CreateVote : ModuleBase<SocketCommandContext>
    {
        [Command("createvote")]
        [Alias("create-vote")]
        public async Task CreateVoteAsync([Remainder] string message)
        {
            await votesDatabase.Votes.AddVoteAsync(Context.Guild, message);

            EmbedBuilder embed = new EmbedBuilder()
                .WithColor(SecurityInfo.botColor)
                .WithTitle($"Poll created")
                .WithDescription($"Please set up the valid reactions with: {Context.Guild.CurrentUser.Mention} setreactions {await votesDatabase.Votes.GetLastVoteIdAsync(Context.Guild)} [reactions]");
            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}
