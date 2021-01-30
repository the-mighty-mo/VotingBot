﻿using Microsoft.Data.Sqlite;
using System.Threading.Tasks;

namespace VotingBot.Databases.MainDatabaseTables
{
    public class DatabaseTable : ITable
    {
        private readonly SqliteConnection connection;

        public DatabaseTable(SqliteConnection connection) => this.connection = connection;

        public Task InitAsync()
        {
            using SqliteCommand cmd = new SqliteCommand("CREATE TABLE IF NOT EXISTS Database (guild_id TEXT PRIMARY KEY, data TEXT NOT NULL);", connection);
            return cmd.ExecuteNonQueryAsync();
        }
    }
}
