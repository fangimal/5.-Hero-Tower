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
        public event Action OnCloseADS;
        public event Action OnErrorVideo;

        private int rewardIndex;
        private bool canGetReward;
        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= GetRewarded;
            YandexGame.CloseFullAdEvent -= CloseIterstisial;
            YandexGame.ErrorVideoEvent -= ErrorReward;
        }

        public void Initialize(StarterAssetsInputs starterAssetsInputs)
        {
            Debug.Log("Initialize AdsService");
            YandexGame.RewardVideoEvent += GetRewarded;
            YandexGame.CloseFullAdEvent += CloseIterstisial;
            YandexGame.ErrorVideoEvent += ErrorReward;

            _starterAssetsInputs = starterAssetsInputs;
        }

        private void ErrorReward()
        {
            Debug.Log("ErrorReward");
            OnErrorVideo?.Invoke();
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
            OnCloseADS?.Invoke();
        }

        public void ShowReward(RewardId rewardType)
        {
            rewardIndex = (int)rewardType;
            canGetReward = true;
            YandexGame.RewVideoShow(rewardIndex);
        }

        private void GetReward()
        {
            GetRewarded(rewardIndex);
        }

        private void GetRewarded(int rewardIndex)
        {
            RewardId rewardType = (RewardId)rewardIndex;

            if (canGetReward)
            {
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

            canGetReward = false;
        }
    }
}