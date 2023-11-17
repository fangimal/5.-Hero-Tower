using System;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.StaticData
{
    [CreateAssetMenu(fileName = "PlayerStaticData", menuName = "Config/PlayerStaticData")]
    public class PlayerStaticData : ScriptableObject
    {
        [SerializeField] private PlayerSkin[] playerGetSkins;
        [SerializeField] private Sprite[] _languageSprites;
        [SerializeField] private Sprite _offIcon;
        [SerializeField] private Sprite _onIcon;
        public PlayerSkin[] GetSkins => playerGetSkins;
        public Sprite[] GetLanguageSprites => _languageSprites;
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
}