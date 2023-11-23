using System;
using StarterAssets;
using UnityEngine;
using YG;

namespace _Scripts.Infrastructure.ADS
{
    public class AdsService : IAdsService
    {
        private StarterAssetsInputs _starterAssetsInputs;
        public event Action OnNextCheckPoint;

        private int rewardIndex;
        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= GetRewarded;
            YandexGame.CloseFullAdEvent -= CloseIterstisial;
        }

        public void Initialize(StarterAssetsInputs starterAssetsInputs)
        {
            Debug.Log("Initialize AdsService");
            YandexGame.RewardVideoEvent += GetRewarded;
            YandexGame.CloseFullAdEvent += CloseIterstisial;
            
            _starterAssetsInputs = starterAssetsInputs;
        }

        public void ShowIterstisial()
        {
            Debug.Log("Show Interstisial");
            
            if (YandexGame.timerShowAd >= YandexGame.Instance.infoYG.fullscreenAdInterval)
            {
                YandexGame.FullscreenShow();
            }
        }

        private void CloseIterstisial()
        {
            _starterAssetsInputs.SetCursour(true);
        }

        public void ShowReward(RewardId rewardType)
        {
            rewardIndex = (int)rewardType;
            Debug.Log("Show Reward");
            YandexGame.RewVideoShow(rewardIndex);
        }

        private void GetReward()
        {
            GetRewarded(rewardIndex);
        }

        private void GetRewarded(int rewardIndex)
        {
            RewardId rewardType = (RewardId)rewardIndex;

            switch (rewardType)
            {
                case RewardId.Checkpoint:
                    Debug.Log("Checkpoint");
                    OnNextCheckPoint?.Invoke();
                    break;
                case RewardId.GetCoin:
                    Debug.Log("GetCoin");
                    break;
            }
        }

    }
}