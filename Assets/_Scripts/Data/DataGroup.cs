using System;

namespace _Scripts.Data
{
    [Serializable]
    public class DataGroup
    {
        public PlayerData playerData;
        
        public DataGroup()
        {
            playerData = new PlayerData();
        }
    }

    [Serializable]
    public class PlayerData
    {
        public int playerSkin;
    }
}