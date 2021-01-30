using VotingBot.Databases.VotesDatabaseTables;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VotingBot.Databases
{
    public class VotesDatabase
    {
        private readonly SqliteConnection connection = new SqliteConnection("Filename=Votes.db");
        private readonly Dictionary<System.Type, ITable> tables = new Dictionary<System.Type, ITable>();

        public VotesTable Votes => tables[typeof(VotesTable)] as VotesTable;
        public ReactionsTable Reactions => tables[typeof(ReactionsTable)] as ReactionsTable;
        public VotersTable Voters => tables[typeof(VotersTable)] as VotersTable;

        public VotesDatabase()
        {
            tables.Add(typeof(VotesTable), new VotesTable(connection));
            tables.Add(typeof(ReactionsTable), new ReactionsTable(connection));
            tables.Add(typeof(VotersTable), new VotersTable(connection));
        }

        public async Task InitAsync()
        {
            await connection.OpenAsync();
            IEnumerable<Task> GetTableInits()
            {
                foreach (var table in tables.Values)
                {
                    yield return table.InitAsync();
                }
            }
            await Task.WhenAll(GetTableInits());
        }

        public async Task CloseAsync() => await connection.CloseAsync();
    }
}