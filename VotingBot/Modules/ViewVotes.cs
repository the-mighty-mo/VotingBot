using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using static VotingBot.DatabaseManager;

namespace VotingBot.Modules
{
    public class ViewVotes : ModuleBase<SocketCommandContext>
    {
        [Command("viewvotes")]
        [Alias("view-votes")]
        public async Task ViewVotesAsync()
        {
            var votes = await votesDatabase.Votes.GetVotesAsync(Context.Guild);

            string voteStr = "";
            foreach (var (id, _) in votes)
            {
                voteStr += $"{id}, ";
            }
            voteStr = voteStr.Length > 1 ? voteStr[0..^2] : "[No votes found]";

            EmbedBuilder embed = new EmbedBuilder()
                .WithTitle("List of Votes")
                .WithColor(SecurityInfo.botColor)
                .WithDescription(voteStr);
            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}
