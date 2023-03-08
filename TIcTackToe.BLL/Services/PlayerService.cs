using Microsoft.EntityFrameworkCore;
using TicTacToe.DataModels.DAL;
using TIcTacToe.BLL.Models.DTO;

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
        private async Task<Player> FindPlayerAsync(int id)
        {
            return await efContext.Players.FindAsync(id) ?? throw new Exception("not found");
        }
        private async Task<Player> FindPlayerAsync(string name)
        {
            return await efContext.Players.FirstOrDefaultAsync(p => p.Name == name) ?? throw new Exception("not found");
        }
        public async Task Update(PlayerDTOUpdate playerDTO)
        {
            var player = await FindPlayerAsync(playerDTO.Id);
            player.Name = playerDTO.Name ?? player.Name;
            efContext.Players.Update(player);
            await efContext.SaveChangesAsync();
        }

        public async Task<PlayerDTOGet> GetPlayerAsync(int id)
        {
            var player = await FindPlayerAsync(id);
            return new PlayerDTOGet { Name = player.Name, Id = player.Id };
        }
        public async Task<PlayerDTOGet> GetPlayerAsync(string name)
        {
            var player = await FindPlayerAsync(name);
            return new PlayerDTOGet { Name = player.Name, Id = player.Id };
        }

        public IEnumerable<PlayerDTOGet> GetPlayers()
        {
            return efContext.Players.Select(p => new PlayerDTOGet
            {
                Id = p.Id,
                Name = p.Name
            }).AsNoTracking();
        }

        public async Task DeletePlayerAsync(int id)
        {
            var player = await efContext.Players.FindAsync(id) ?? throw new Exception("not found");
            efContext.Players.Remove(player);
            await efContext.SaveChangesAsync();
        }
    }
}
