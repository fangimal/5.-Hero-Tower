using System;
using StarterAssets;
using UnityEngine;

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
            VKProvider.Instance.OnGetReward -= GetReward;
            VKProvider.Instance.OnErrorReward -= ErrorReward;
            VKProvider.Instance.OnCloseInterstitial -= CloseIterstisial;
        }

        public void Initialize(StarterAssetsInputs starterAssetsInputs)
        {
            Debug.Log("Initialize AdsService");

            VKProvider.Instance.OnGetReward += GetReward;
            VKProvider.Instance.OnErrorReward += ErrorReward;
            VKProvider.Instance.OnCloseInterstitial += CloseIterstisial;
            
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
            VKProvider.Instance.VKShowAdv();
        }
        
        private void CloseIterstisial()
        {
            _starterAssetsInputs.SetCursour(true);
            OnCloseADS?.Invoke();
        }

        public void ShowReward(RewardId rewardType)
        {
            rewardIndex = (int)rewardType;
            
            Debug.Log("Show Reward");
            canGetReward = true;
            //GetRewarded(rewardIndex); // TODO dell when add reward action

            VKProvider.Instance.VKRewardAdv();
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