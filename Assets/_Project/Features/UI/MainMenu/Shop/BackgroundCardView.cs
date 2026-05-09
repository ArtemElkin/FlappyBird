using System.Collections.Generic;
using _Project.Features.Gameplay.Background;
using UnityEngine;
using UnityEngine.UI;


namespace _Project.Features.UI.MainMenu.Shop
{
    public class BackgroundCardView : MonoBehaviour
    {
        [SerializeField] private List<Image> _backgroundLayersImages;
        [SerializeField] private PricePanel _pricePanel;
        
        
        public void UpdateCard(BackgroundConfig backgroundConfig, bool unlocked)
        {
            ClearCard();
            for (int i = 0; i < backgroundConfig.backgroundLayers.Count; i++)
            {
                _backgroundLayersImages[i].sprite = backgroundConfig.backgroundLayers[i].sprite;
            }
            if (unlocked)
            {
                _pricePanel.Hide();
            }
            else
            {
                _pricePanel.Show();
                _pricePanel.UpdatePrice(backgroundConfig.price);
            }
        }

        private void ClearCard()
        {
            foreach (var backgroundLayer in _backgroundLayersImages)
            {
                backgroundLayer.sprite = null;
            }
        }
    }
}