using System;
using UnityEngine;

namespace _Scripts.Infrastructure.ADS
{
    public class AdsService : IAdsService
    {
        public event Action OnNextCheckPoint;

        private int rewardIndex;
        public void Initialize()
        {
            Debug.Log("Initialize AdsService");
        }

        public void ShowIterstisial()
        {
            Debug.Log("Show Interstisial");
        }

        public void ShowReward(RewardId rewardType)
        {
            rewardIndex = (int)rewardType;
            
            Debug.Log("Show Reward");

            GetRewarded(rewardIndex); // TODO dell when add reward action
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