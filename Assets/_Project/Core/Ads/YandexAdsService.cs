using System;
using System.Threading.Tasks;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;
using Zenject;

namespace _Project.Core.Ads
{
    public class YandexAdsService : IAdsService, IDisposable
    {
        private const string BannerAdUnitId = "demo-banner-yandex";
        private const string InterstitialAdUnitId = "demo-interstitial-yandex";
        private const string RewardedAdUnitId = "demo-rewarded-yandex";
        private Banner _bannerAd;
        private Interstitial _interstitialAd;
        private RewardedAd _rewardedAd;
        private InterstitialAdLoader _interstitialAdLoader;
        private RewardedAdLoader _rewardedAdLoader;
        public bool IsInterstitialReady() => _interstitialAd != null;
        public bool IsRewardedReady() => _rewardedAd != null;
        public bool IsBannerReady() => _bannerAd != null;
        
        
        
        public void Initialize()
        {
            YandexAds.SetAgeRestricted(true);
            
            _interstitialAdLoader = new InterstitialAdLoader();
            _rewardedAdLoader = new RewardedAdLoader();
            
            RequestBanner();
            HideBanner();
            _ = LoadInterstitial();
            _ = LoadRewarded();
            
        }
        
        public async Task LoadInterstitial()
        {
            DestroyInterstitial();
            try
            {
                Debug.Log("Yandex Ads: Start loading interstitial...");
                _interstitialAd = await _interstitialAdLoader.LoadAd(new AdRequest(InterstitialAdUnitId));
                Debug.Log("Yandex Ads: Interstitial loaded successfully");
            }
            catch (AdLoadingException e)
            {
                Debug.LogError($"Yandex Ads: Interstitial failed to load: {e.Message}");
            }
        }

        public async Task LoadRewarded()
        {
            DestroyRewarded();
            try
            {
                _rewardedAd = await _rewardedAdLoader.LoadAd(new AdRequest(RewardedAdUnitId));
            }
            catch (AdLoadingException e)
            {
                // Ad failed to load with {e.Message}
                // Attempting to load a new ad from catch block is strongly discouraged.
            }
        }

        public void ShowInterstitial()
        {
            if (IsInterstitialReady())
            {
                _interstitialAd.Show();
                _ = LoadInterstitial();
            }
            else
            {
                Debug.LogWarning("Yandex Ads: Interstitial not ready yet!");
            }
        }

        public void ShowRewarded(Action onRewardSuccess)
        {
            if (!IsRewardedReady())
            {
                Debug.LogWarning("[Yandex Ads] Rewarded not ready yet!");
                return;
            }
            bool isRewardEarned = false;
            _rewardedAd.OnRewarded += (sender, args) => 
            {
                isRewardEarned = true;
                Debug.Log("[Yandex Ads] Пользователь досмотрел до конца!");
            };

            _rewardedAd.OnAdDismissed += (sender, args) => 
            {
                Debug.Log("[Yandex Ads] Реклама закрыта.");
                if (isRewardEarned)
                {
                    onRewardSuccess?.Invoke();
                }
                _ = LoadRewarded();
            };
            _rewardedAd.OnAdFailedToShow += (sender, args) => 
            {
                Debug.LogError($"[Yandex Ads] Не удалось показать: {args.Message}");
                _ = LoadRewarded();
            };
            _rewardedAd.Show();
        }

        public void ShowBanner()
        {
            if (IsBannerReady())
            {
                _bannerAd.Show();
            }
            else
            {
                RequestBanner();
            }
        }
        
        public void HideBanner()
        {
            _bannerAd?.Hide();
        }
        
        private int GetScreenWidthDp()
        {
            int screenWidth = (int)Screen.safeArea.width;
            return ScreenUtils.ConvertPixelsToDp(screenWidth);
        }

        private void HandleImpression(object sender, ImpressionData e)
        {
            throw new NotImplementedException();
        }

        private void HandleAdClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HandleAdFailedToLoad(object sender, AdFailureEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HandleAdLoaded(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        
        private void RequestBanner()
        {
            // Set sticky banner width
            BannerAdSize bannerSize = BannerAdSize.Sticky(GetScreenWidthDp());
            // Or set inline banner maximum width and height
            // BannerAdSize bannerSize = BannerAdSize.Inline(GetScreenWidthDp(), 300);
            _bannerAd = new Banner(bannerSize, AdPosition.BottomCenter);

            _bannerAd.OnAdLoaded += HandleAdLoaded;
            _bannerAd.OnAdFailedToLoad += HandleAdFailedToLoad;
            _bannerAd.OnAdClicked += HandleAdClicked;
            _bannerAd.OnImpression += HandleImpression;
            
            var request = new AdRequest(BannerAdUnitId);
            
            _bannerAd.LoadAd(request);
        }

        private void DestroyBanner()
        {
            if (_bannerAd != null)
            {
                _bannerAd.Destroy();
                _bannerAd = null;
            }
        }

        private void DestroyInterstitial()
        {
            if (_interstitialAd != null)
            {
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }
        }

        private void DestroyRewarded()
        {
            if (_rewardedAd != null)
            {
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }
        }
        public void Dispose()
        {
            DestroyBanner();
            DestroyInterstitial();
            DestroyRewarded();
        }
    }
}