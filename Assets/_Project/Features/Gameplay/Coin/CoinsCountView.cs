using _Project.Core.Data;
using TMPro;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Coin
{
    public class CoinsCountView : MonoBehaviour
    {
        private TextMeshProUGUI _coinsText;
        private PlayerModel _playerModel;
        

        private void OnEnable()
        {
            _playerModel.OnCoinsChanged += UpdateCoinsCountView;
        }
        
        [Inject]
        private void Construct(PlayerModel playerModel)
        {
            _playerModel = playerModel;
            _coinsText = GetComponent<TextMeshProUGUI>();
            UpdateCoinsCountView(_playerModel.Coins);
        }
        
        private void UpdateCoinsCountView(int value)
        {
            _coinsText.text = value + " $";
        }

        private void OnDisable()
        {
            _playerModel.OnCoinsChanged -= UpdateCoinsCountView;
        }
    }
}