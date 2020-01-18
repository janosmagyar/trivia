using System.Collections.Generic;

namespace Trivia
{
    public class WinningConditions
    {
        private static readonly Dictionary<PlayerType,int> CoinsToWin= new Dictionary<PlayerType, int>()
        {
            {PlayerType.Adult,6},
            {PlayerType.Kid, 4},
        };

        public bool IsWining(Player player)
        {
            return player.Purse >= CoinsToWin[player.PlayerType];
        }
    }
}