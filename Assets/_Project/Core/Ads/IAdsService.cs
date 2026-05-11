using System;

namespace _Project.Core.Ads
{
    public interface IAdsService
    {
        // Загрузка
        void LoadInterstitial();
        void LoadRewarded();

        // Проверка готовности
        bool IsInterstitialReady();
        bool IsRewardedReady();

        // Показ
        void ShowInterstitial();
        void ShowRewarded(Action onRewardSuccess); // Callback для выдачи золота/жизней

        // Баннеры
        void ShowBanner();
        void HideBanner();
    }
}