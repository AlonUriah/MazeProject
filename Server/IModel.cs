using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public interface IModel
    {
        List<string> GetAvailableGames();
        Game AddSinglePlayerGame(string name, int rows, int cols);
        Game AddMultiPlayerGame(Player client, string name, int rows, int cols);
        Player[] DeleteMultiPlayerGame(string name);
        Game JoinMultiPlayerGame(Player client, string name);
        Player GetRival(Player player);
        string GetGame(Player player);
    }
}
