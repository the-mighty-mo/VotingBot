using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using static VotingBot.DatabaseManager;

namespace VotingBot.Modules
{
    public class ViewVote : ModuleBase<SocketCommandContext>
    {
        [Command("viewvote")]
        [Alias("view-vote")]
        public async Task ViewVoteAsync(int voteId)
        {
            string message = await votesDatabase.Votes.GetVoteAsync(Context.Guild, voteId);
            if (message == null)
            {
                await Context.Channel.SendMessageAsync($"Error: no vote with id {voteId} found");
                return;
            }

            EmbedBuilder embed = new EmbedBuilder()
                .WithTitle($"Vote {voteId} Information")
                .WithColor(SecurityInfo.botColor);

            EmbedFieldBuilder messageField = new EmbedFieldBuilder()
                .WithIsInline(false)
                .WithName("Message")
                .WithValue(message);
            embed.AddField(messageField);

            string reactions = "[No reactions have been set up]";

            EmbedFieldBuilder reactionsField = new EmbedFieldBuilder()
                .WithIsInline(false)
                .WithName("Reactions")
                .WithValue(reactions);
            embed.AddField(reactionsField);

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}
