using VotingBot.Databases;
using System.Threading.Tasks;

namespace VotingBot
{
    public static class DatabaseManager
    {
        public static readonly VotesDatabase votesDatabase = new VotesDatabase();

        public static async Task InitAsync()
        {
            await Task.WhenAll(
                votesDatabase.InitAsync()
            );
        }

        public static async Task CloseAsync()
        {
            await Task.WhenAll(
                votesDatabase.CloseAsync()
            );
        }
    }
}