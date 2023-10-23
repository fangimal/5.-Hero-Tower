using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class SkinPanelUI : MonoBehaviour
    {
        [SerializeField] private Button backStartButton;
        
        private GameObject player;

        public event Action OnBackStart;

        private void Awake()
        {
            backStartButton.onClick.AddListener(()=>OnBackStart?.Invoke());
        }

        public void Init(GameObject player)
        {
            this.player = player;
        }
    }
}