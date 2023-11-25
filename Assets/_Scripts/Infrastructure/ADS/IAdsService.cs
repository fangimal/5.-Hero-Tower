using System;
using _Scripts.Infrastructure.Services;
using StarterAssets;

namespace _Scripts.Infrastructure.ADS
{
    public interface IAdsService : IService
    {
        void Initialize(StarterAssetsInputs starterAssetsInputs);
        void ShowIterstisial();
        void ShowReward(RewardId rewardType);

        event Action OnNextCheckPoint;

        public event Action OnCloseADS;
    }
}