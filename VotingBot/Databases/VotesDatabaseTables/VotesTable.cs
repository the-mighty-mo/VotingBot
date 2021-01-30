using Discord.WebSocket;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VotingBot.Databases.VotesDatabaseTables
{
    public class VotesTable : ITable
    {
        private readonly SqliteConnection connection;

        public VotesTable(SqliteConnection connection) => this.connection = connection;

        public Task InitAsync()
        {
            using SqliteCommand cmd = new SqliteCommand("CREATE TABLE IF NOT EXISTS Votes (guild_id TEXT PRIMARY KEY, vote_id INTEGER NOT NULL, message TEXT NOT NULL, UNIQUE(guild_id, vote_id));", connection);
            return cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> GetLastVoteIdAsync(SocketGuild g)
        {
            int lastVoteId = 0;

            string getVoteId = "SELECT MAX(vote_id) AS vote_id FROM Votes WHERE guild_id = @guild_id;";
            using (SqliteCommand cmd = new SqliteCommand(getVoteId, connection))
            {
                cmd.Parameters.AddWithValue("@guild_id", g.Id);

                SqliteDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    int.TryParse(reader["vote_id"].ToString()!, out lastVoteId);
                }
                reader.Close();
            }

            return lastVoteId;
        }

        public async Task<string> GetVoteAsync(SocketGuild g, int voteId)
        {
            string message = null;

            string getVoteId = "SELECT message FROM Votes WHERE guild_id = @guild_id AND vote_id = @vote_id;";
            using (SqliteCommand cmd = new SqliteCommand(getVoteId, connection))
            {
                cmd.Parameters.AddWithValue("@guild_id", g.Id);
                cmd.Parameters.AddWithValue("@vote_id", voteId);

                SqliteDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    message = reader["message"].ToString();
                }
                reader.Close();
            }

            return message;
        }

        public async Task<List<(int id, string msg)>> GetVotesAsync(SocketGuild g)
        {
            List<(int, string)> votes = new List<(int, string)>();

            string getVoteId = "SELECT vote_id, message FROM Votes WHERE guild_id = @guild_id;";
            using (SqliteCommand cmd = new SqliteCommand(getVoteId, connection))
            {
                cmd.Parameters.AddWithValue("@guild_id", g.Id);

                SqliteDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    votes.Add((int.Parse(reader["vote_id"].ToString()!), reader["message"].ToString()));
                }
                reader.Close();
            }

            return votes;
        }

        public async Task AddVoteAsync(SocketGuild g, string message)
        {
            string insert = "INSERT INTO Votes (guild_id, vote_id, message) SELECT @guild_id, @vote_id, @message\n" +
                "WHERE NOT EXISTS (SELECT 1 FROM Votes WHERE guild_id = @guild_id AND vote_id = @vote_id);";

            using (SqliteCommand cmd = new SqliteCommand(insert, connection))
            {
                cmd.Parameters.AddWithValue("@guild_id", g.Id);
                cmd.Parameters.AddWithValue("@vote_id", await GetLastVoteIdAsync(g) + 1);
                cmd.Parameters.AddWithValue("@message", message);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateVoteMessageAsync(SocketGuild g, int voteId, string message)
        {
            string insert = "UPDATE Votes SET message = @message WHERE guild_id = @guild_id AND vote_id = @vote_id;";
            using (SqliteCommand cmd = new SqliteCommand(insert, connection))
            {
                cmd.Parameters.AddWithValue("@guild_id", g.Id);
                cmd.Parameters.AddWithValue("@vote_id", voteId);
                cmd.Parameters.AddWithValue("@message", message);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task RemoveVotesAsync(SocketGuild g)
        {
            string delete = "DELETE FROM Votes WHERE guild_id = @guild_id;";
            using (SqliteCommand cmd = new SqliteCommand(delete, connection))
            {
                cmd.Parameters.AddWithValue("@guild_id", g.Id);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
