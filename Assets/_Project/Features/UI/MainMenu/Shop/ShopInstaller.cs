using _Project.Features.UI.MainMenu.Shop.Buttons;
using _Project.Features.UI.Signals;
using UnityEngine;
using Zenject;


namespace _Project.Features.UI.MainMenu.Shop
{
    public class ShopInstaller : MonoInstaller
    {
        [SerializeField] private BackgroundCardView _backgroundCardView;
        [SerializeField] private BuyChooseBackgroundButton _buyChooseBackgroundButton;
        
        
        public override void InstallBindings()
        {
            Container.DeclareSignal<BuyChooseBackgroundButtonClickedSignal>();
            Container.DeclareSignal<NextBackgroundButtonClickedSignal>();
            Container.DeclareSignal<PreviousBackgroundButtonClickedSignal>();
            
            BindShop(_backgroundCardView, _buyChooseBackgroundButton);
        }

        private void BindShop(BackgroundCardView backgroundCardView, BuyChooseBackgroundButton buyChooseBackgroundButton)
        {
            Container
                .BindInterfacesAndSelfTo<Shop>()
                .AsSingle()
                .WithArguments(backgroundCardView, buyChooseBackgroundButton);
        }
    }
}