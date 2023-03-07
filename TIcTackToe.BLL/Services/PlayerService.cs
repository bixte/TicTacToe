using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TIcTackToe.BLL.Models.DTO;
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

        public async Task<PlayerDTOGet> GetPlayerAsync(int id)
        {
            var player = await efContext.Players.FindAsync(id) ?? throw new Exception("not found");
            return new PlayerDTOGet { Name = player.Name };
        }

        public IEnumerable<PlayerDTOGet> GetPlayers()
        {
            return efContext.Players.Select(p => new PlayerDTOGet
            {
                Name = p.Name
            }).AsNoTracking();
        }

        public async Task DeletePlayerAsync(int id)
        {
            var player =  await efContext.Players.FindAsync(id) ?? throw new Exception("not found");
            efContext.Players.Remove(player);
        }
    }
}
