using System.Threading.Tasks;

namespace VotingBot.Databases
{
    interface ITable
    {
        public Task InitAsync();
    }
}
