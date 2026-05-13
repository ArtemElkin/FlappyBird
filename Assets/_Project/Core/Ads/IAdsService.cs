using System;
using System.Threading.Tasks;


namespace _Project.Core.Ads
{
    public interface IAdsService
    {
        void Initialize();
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