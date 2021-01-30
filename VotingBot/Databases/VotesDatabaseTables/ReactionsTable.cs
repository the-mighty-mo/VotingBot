using Microsoft.Data.Sqlite;
using System.Threading.Tasks;

namespace VotingBot.Databases.VotesDatabaseTables
{
    public class ReactionsTable : ITable
    {
        private readonly SqliteConnection connection;

        public ReactionsTable(SqliteConnection connection) => this.connection = connection;

        public Task InitAsync()
        {
            using SqliteCommand cmd = new SqliteCommand("CREATE TABLE IF NOT EXISTS Reactions (guild_id TEXT PRIMARY KEY, vote_id INTEGER NOT NULL, reaction TEXT NOT NULL, UNIQUE(guild_id, vote_id, reaction));", connection);
            return cmd.ExecuteNonQueryAsync();
        }
    }
}
