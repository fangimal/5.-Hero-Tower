using System;
using System.Collections.Generic;
using _Scripts.StaticData;
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
        private int selectedSkin;

        private void Awake()
        {
            _backStartButton.onClick.AddListener(() =>
            {
                OnClickedPlay(AudioClipName.Btn);
                _player.SetVisualize(PlayerData.playerSkin);
                Close();
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

        public void Close()
        {
            foreach (SkinItem item in _skinItems)
            {
                Destroy(item.gameObject);
            }
            
            _skinItems.Clear();
        }

        private void CreateItems()
        {
            if (_skinItems.Count == 0)
            {
                for (int i = 0; i < _playerStaticData.GetSkins.Length; i++)
                {
                    SkinItem item = Instantiate(_skinItemPrefab, _content);
                    bool locked = !PlayerData.openSkin.Contains(i);
                    item.Init(_playerStaticData.GetSkins[i].Sprite, i, locked);
                    item.SetSelected(PlayerData.playerSkin == i);
                    item.OnClicked += ClickedSkinItem;
                    _skinItems.Add(item);
                }
            }
        }

        public void ClickedSkinItem(int index)
        {
            OnClickedPlay(AudioClipName.Btn);
            _player.SetVisualize(index);
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
            if (PlayerData.Coins >= _playerStaticData.GetSkins[selectedSkin].Price && PlayerData.Coins !=0)
            {
                PlayerData.AddCoins(-_playerStaticData.GetSkins[selectedSkin].Price);
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
                _price.text = _playerStaticData.GetSkins[skinIndex].Price.ToString();
            }
        }
    }
}