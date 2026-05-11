using System;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;
using Zenject;

namespace _Project.Core.Ads
{
    public class YandexAdsService : IAdsService, IInitializable
    {
        private Banner _banner;
        private string _adUnitId;
        
        
        public void Initialize()
        {
            YandexAds.SetAgeRestricted(true);
            _adUnitId = "demo-banner-yandex";
            
            RequestBanner();
        }
        
        public void LoadInterstitial()
        {
            throw new NotImplementedException();
        }

        public void LoadRewarded()
        {
            throw new NotImplementedException();
        }

        public bool IsInterstitialReady()
        {
            throw new NotImplementedException();
        }

        public bool IsRewardedReady()
        {
            throw new NotImplementedException();
        }

        public void ShowInterstitial()
        {
            throw new NotImplementedException();
        }

        public void ShowRewarded(Action onRewardSuccess)
        {
            throw new NotImplementedException();
        }

        public void ShowBanner()
        {
            if (_banner != null)
            {
                _banner.Show();
            }
            else
            {
                RequestBanner();
            }
        }
        
        public void HideBanner()
        {
            _banner?.Hide();
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
            this._banner = new Banner(bannerSize, AdPosition.BottomCenter);

            this._banner.OnAdLoaded += this.HandleAdLoaded;
            this._banner.OnAdFailedToLoad += this.HandleAdFailedToLoad;
            this._banner.OnAdClicked += this.HandleAdClicked;
            this._banner.OnImpression += this.HandleImpression;

            this._banner.LoadAd(this.CreateAdRequest(_adUnitId));
        }
        
        private AdRequest CreateAdRequest(string adUnitId)
        {
            return new AdRequest(adUnitId);
        }
    }
}