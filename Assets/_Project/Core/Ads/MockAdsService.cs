using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Core.Ads
{
    public class MockAdsService : IAdsService, IInitializable, IDisposable
    {
        public void Initialize()
        {
            Debug.Log("MockAdsService.Initialize");
        }
        
        public Task LoadInterstitial()
        {
            Debug.Log("MockAdsService.LoadInterstitial");
            return Task.CompletedTask;
        }

        public Task LoadRewarded()
        {
            Debug.Log("MockAdsService.LoadRewarded");
            return Task.CompletedTask;
        }

        public bool IsInterstitialReady()
        {
            return true;
        }

        public bool IsRewardedReady()
        {
            return true;
        }

        public bool IsBannerReady()
        {
            return true;
        }

        public void ShowInterstitial()
        {
            Debug.Log("MockAdsService.ShowInterstitial");
        }

        public void ShowRewarded(Action onRewardSuccess)
        {
            Debug.Log("MockAdsService.ShowRewarded");
            
            onRewardSuccess?.Invoke();
        }

        public void ShowBanner()
        {
            Debug.Log("MockAdsService.ShowBanner");
        }

        public void HideBanner()
        {
            Debug.Log("MockAdsService.HideBanner");
        }

        public void Dispose()
        {
            
        }
    }
}