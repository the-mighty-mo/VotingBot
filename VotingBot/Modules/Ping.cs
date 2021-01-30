﻿using Discord;
using Discord.Commands;
using Discord.Rest;
using System.Threading.Tasks;

namespace VotingBot.Modules
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync()
        {
            EmbedBuilder embed = new EmbedBuilder()
                .WithColor(SecurityInfo.botColor)
                .WithDescription(":ping_pong:**Pong!**");
            RestUserMessage msg = await Context.Channel.SendMessageAsync(embed: embed.Build());

            embed.WithDescription(":ping_pong:**Pong!**\n" +
                $"**Server:** {(int)(msg.Timestamp - Context.Message.Timestamp).TotalMilliseconds}ms\n" +
                $"**API:** {Context.Client.Latency}ms");

            await msg.ModifyAsync(x => x.Embed = embed.Build());
        }
    }
}