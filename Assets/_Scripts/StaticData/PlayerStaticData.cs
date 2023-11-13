using System;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.StaticData
{
    [CreateAssetMenu(fileName = "PlayerStaticData", menuName = "Config/PlayerStaticData")]
    public class PlayerStaticData : ScriptableObject
    {
        [SerializeField] private PlayerSkin[] _playerSkin;

        public PlayerSkin[] Skin => _playerSkin;
    }

    [Serializable]

    public class PlayerSkin
    {
        public Sprite Sprite;
        public int Price;
        public Visualize VisualizerPrefag;
    }
}