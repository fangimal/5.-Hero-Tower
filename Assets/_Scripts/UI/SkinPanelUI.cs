using System;
using System.Collections.Generic;
using _Scripts.StaticData;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class SkinPanelUI : WindowBase
    {
        [SerializeField] private Button _backStartButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Transform _content;
        [SerializeField] private SkinItem _skinItemPrefab;

        private List<SkinItem> _skinItems = new List<SkinItem>();
        public event Action OnBackStart;

        private void Awake()
        {
            _backStartButton.onClick.AddListener(() =>
                {
                    OnBackStart?.Invoke();
                });
        }

        public void Open()
        {
            CreateItems();
        }

        private void CreateItems()
        {
            if (_skinItems.Count == 0)
            {
                PlayerStaticData playerStaticData = player.GetComponent<ThirdPersonController>().PlayerStaticData;

                for (int i = 0; i < playerStaticData.Skin.Length; i++)
                {
                    SkinItem item = Instantiate(_skinItemPrefab, _content);
                    bool locked = false; //TODO set bool
                    item.Init(playerStaticData.Skin[i].Sprite, i, locked);
                    item.OnClicked += ClickedSkinItem;
                    _skinItems.Add(item);
                }
            }
        }

        private void ClickedSkinItem(int i)
        {
            player.SetVisualize(i);
            PlayerData.playerSkin = i;
            _saveLoadService.SaveProgress();
            Debug.Log("ClickedSkinItem: " + i);
        }
    }
}