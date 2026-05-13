using System;
using System.Threading.Tasks;
using _Project.Core.Infrastructure.Config;


namespace _Project.Core.Ads
{
    public interface IAdsService
    {
        void Initialize(AdUnitsIdsConfig config);
        Task LoadInterstitial();
        Task LoadRewarded();
        bool IsInterstitialReady();
        bool IsRewardedReady();
        bool IsBannerReady();
        void ShowInterstitial();
        void ShowRewarded(Action onRewardSuccess);
        void ShowBanner();
        void HideBanner();
    }
}