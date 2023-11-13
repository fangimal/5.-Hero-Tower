using System;
using _Scripts.Infrastructure.Services;

namespace _Scripts.Infrastructure.ADS
{
    public interface IAdsService : IService
    {
        void Initialize();
        void ShowIterstisial();
        void ShowReward(RewardId rewardType);

        event Action OnNextCheckPoint;
    }
}