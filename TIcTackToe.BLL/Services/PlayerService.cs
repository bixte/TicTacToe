using System.Collections.Generic;
using TicTacToe.DataModels.DAL;

namespace TIcTackToe.BLL.Services
{
    public class PlayerService
    {
        private readonly EfContext efContext;

        public PlayerService(EfContext efContext)
        {
            this.efContext = efContext;
        }
        public async Task CreateAsync(string name)
        {
            var player = efContext.Players.FirstOrDefault(p => p.Name == name);
            if (player != null)
                throw new Exception("this name is taken");

            await efContext.Players.AddAsync(
                new Player
                {
                    Name = name
                });
            await efContext.SaveChangesAsync();
        }

        public async Task Update(Player player)
        {
            efContext.Players.Update(player);
            await efContext.SaveChangesAsync();
        }

        public async Task<Player> GetPlayer(int id)
        {
            return await efContext.Players.FindAsync(id) ?? throw new Exception("not found");
        }

        public IEnumerable<Player> GetPlayers()
        {
            return efContext.Players;
        }
    }
}
