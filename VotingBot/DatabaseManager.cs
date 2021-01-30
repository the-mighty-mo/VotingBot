using BotBase.Databases;
using System.Threading.Tasks;

namespace BotBase
{
    public static class DatabaseManager
    {
        public static readonly MainDatabase database = new MainDatabase();

        public static async Task InitAsync()
        {
            await Task.WhenAll(
                database.InitAsync()
            );
        }

        public static async Task CloseAsync()
        {
            await Task.WhenAll(
                database.CloseAsync()
            );
        }
    }
}