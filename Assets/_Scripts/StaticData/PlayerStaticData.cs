using System;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.StaticData
{
    [CreateAssetMenu(fileName = "PlayerStaticData", menuName = "Config/PlayerStaticData")]
    public class PlayerStaticData : ScriptableObject
    {
        [SerializeField] private PlayerSkin[] playerGetSkins;
        
        [Header ("Settings")]
        [SerializeField] private Sprite _offIcon;
        [SerializeField] private Sprite _onIcon;
        
        [Header ("Local")]
        [SerializeField] private Local[] _locals;
        public PlayerSkin[] GetSkins => playerGetSkins;
        public Local[] GetLocals => _locals;
        public Sprite GetOffIcon => _offIcon;
        public Sprite GetOnIcon => _onIcon;
    }

    [Serializable]
    public class PlayerSkin
    {
        public Sprite Sprite;
        public int Price;
        public Visualize VisualizerPrefag;
    }

    [Serializable]
    public class Local
    {
        public string langCode;
        public Sprite langSprite;
    }
}