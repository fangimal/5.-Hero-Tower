using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.StaticData;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class SkinPanelUI : WindowBase
    {
        [SerializeField] private Button _backStartButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private Transform _content;
        [SerializeField] private SkinItem _skinItemPrefab;
        public event Action OnBackStart;
        
        private List<SkinItem> _skinItems = new List<SkinItem>();
        private PlayerStaticData _playerStaticData;
        private int selectedSkin;

        private void Awake()
        {
            _backStartButton.onClick.AddListener(() =>
            {
                OnClickedPlay(AudioClipName.Btn);
                player.SetVisualize(PlayerData.playerSkin);
                OnBackStart?.Invoke();
            });

            _buyButton.onClick.AddListener(BuySkin);
        }

        public void Open()
        {
            selectedSkin = PlayerData.playerSkin;
            CreateItems();
            CheckCanBuy(selectedSkin);
        }

        private void CreateItems()
        {
            if (_skinItems.Count == 0)
            {
                _playerStaticData = player.GetComponent<ThirdPersonController>().PlayerStaticData;

                for (int i = 0; i < _playerStaticData.Skin.Length; i++)
                {
                    SkinItem item = Instantiate(_skinItemPrefab, _content);
                    bool locked = !PlayerData.openSkin.Contains(i);
                    item.Init(_playerStaticData.Skin[i].Sprite, i, locked);
                    item.SetSelected(PlayerData.playerSkin == i);
                    item.OnClicked += ClickedSkinItem;
                    _skinItems.Add(item);
                }
            }
        }

        private void ClickedSkinItem(int index)
        {
            OnClickedPlay(AudioClipName.Btn);
            player.SetVisualize(index);
            ChangeSelectedItem(index);
            CheckBuyingSkin(index);
            Debug.Log("ClickedSkinItem: " + index);
        }

        private void ChangeSelectedItem(int itemIndex)
        {
            foreach (SkinItem item in _skinItems)
            {
                item.SetSelected(item.GetIndex == itemIndex);
            }

            CheckCanBuy(itemIndex);
            selectedSkin = itemIndex;
        }

        private void CheckBuyingSkin(int skinIndex)
        {
            if (PlayerData.openSkin.Contains(skinIndex))
            {
                SaveSkin(skinIndex);
            }
        }

        private void SaveSkin(int skinIndex)
        {
            PlayerData.playerSkin = skinIndex;
            _saveLoadService.SaveProgress();
        }

        private void BuySkin()
        {
            if (PlayerData.Coins >= _playerStaticData.Skin[selectedSkin].Price && PlayerData.Coins !=0)
            {
                PlayerData.AddCoins(-_playerStaticData.Skin[selectedSkin].Price);
                _skinItems[selectedSkin].BuySkin();
                PlayerData.openSkin.Add(selectedSkin);
                SaveSkin(selectedSkin);

                OnClickedPlay(AudioClipName.Coins);
            }

            CheckCanBuy(selectedSkin);
        }

        private void CheckCanBuy(int skinIndex)
        {
            bool canBuy = !PlayerData.openSkin.Contains(skinIndex);
            _buyButton.gameObject.SetActive(canBuy);
            
            if (canBuy)
            {
                _price.text = _playerStaticData.Skin[skinIndex].Price.ToString();
            }
        }
    }
}