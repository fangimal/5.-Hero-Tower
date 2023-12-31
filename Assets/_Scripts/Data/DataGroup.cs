﻿using System;
using System.Collections.Generic;

namespace _Scripts.Data
{
    [Serializable]
    public class PlayerData
    {
        public bool isFirstLoad = true;
        public bool isSoundOn = true;
        public bool isMusicOn = true;
        public int langIndex = 0;
        public int Coins = 50;
        public int playerSkin;
        public List<int> checkpointIndex = new List<int>(){-1};
        public List<int> openSkin = new List<int>() { 0 };

        public Action CoinsChanged;
        public void AddCoins(int coins)
        {
            Coins += coins;
            CoinsChanged?.Invoke();
        }
    }
}