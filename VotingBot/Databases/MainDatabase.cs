﻿using VotingBot.Databases.MainDatabaseTables;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VotingBot.Databases
{
    public class MainDatabase
    {
        private readonly SqliteConnection connection = new SqliteConnection("Filename=Database.db");
        private readonly Dictionary<System.Type, ITable> tables = new Dictionary<System.Type, ITable>();

        public DatabaseTable Database => tables[typeof(DatabaseTable)] as DatabaseTable;

        public MainDatabase()
        {
            tables.Add(typeof(DatabaseTable), new DatabaseTable(connection));
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