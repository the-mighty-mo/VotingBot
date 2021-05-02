using Microsoft.Data.Sqlite;
using System.Threading.Tasks;

namespace VotingBot.Databases.VotesDatabaseTables
{
    public class VotersTable : ITable
    {
        private readonly SqliteConnection connection;

        public VotersTable(SqliteConnection connection) => this.connection = connection;

        public Task InitAsync()
        {
            using SqliteCommand cmd = new("CREATE TABLE IF NOT EXISTS Voters (guild_id TEXT PRIMARY KEY, vote_id INTEGER NOT NULL, user_id TEXT NOT NULL, reaction TEXT NOT NULL, UNIQUE(guild_id, vote_id, user_id));", connection);
            return cmd.ExecuteNonQueryAsync();
        }
    }
}
