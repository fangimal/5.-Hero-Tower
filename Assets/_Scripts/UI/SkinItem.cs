using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class SkinItem : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _selectedImage;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _lockedImage;
        
        private int index;
        public int GetIndex => index;
        public event Action<int> OnClicked; 
        private void Awake()
        {
            _button.onClick.AddListener(()=>OnClicked?.Invoke(index));
        }

        public void Init(Sprite icon, int currentIndex, bool isLocked)
        {
            _iconImage.sprite = icon;
            index = currentIndex;
            _lockedImage.gameObject.SetActive(isLocked);
        }

        public void SetSelected(bool isSelected)
        {
            _selectedImage.gameObject.SetActive(isSelected);
        }
    }
}