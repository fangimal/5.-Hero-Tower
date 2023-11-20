using System;
using System.Collections.Generic;
using YG;

namespace _Scripts.Data
{
    [Serializable]
    public class PlayerData
    {
        public bool isSoundOn = true;
        public bool isMusicOn = true;
        public int langIndex = 0;
        public int Coins = 100;
        public int playerSkin;
        private int coinsForLiderBoard;
        public List<int> checkpointIndex = new List<int>(){-1};
        public List<int> openSkin = new List<int>() { 0 };

        public Action CoinsChanged;
        public void AddCoins(int coins)
        {
            Coins += coins;
            
            if (coins > 0)
            {
                coinsForLiderBoard += coins;
                YandexGame.NewLeaderboardScores("coins", coinsForLiderBoard);
            }
            
            CoinsChanged?.Invoke();
        }
    }
}