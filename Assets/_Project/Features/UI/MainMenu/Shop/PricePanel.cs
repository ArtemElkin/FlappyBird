using TMPro;
using UnityEngine;


namespace _Project.Features.UI.MainMenu.Shop
{
    public class PricePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _priceText;

        
        public void UpdatePrice(int price)
        {
            _priceText.text = price + " $";
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

    }
}