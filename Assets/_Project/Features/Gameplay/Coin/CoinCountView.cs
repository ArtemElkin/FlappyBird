using _Project.Core.Data;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Coin
{
    public class CoinCountView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _goldText;
        private PlayerModel _playerModel;

        private void Awake()
        {
            _goldText = GetComponent<TextMeshProUGUI>();
            UpdateCoinsCountView(_playerModel.Coins);
        }

        private void OnEnable()
        {
            _playerModel.OnCoinsChanged += UpdateCoinsCountView;
        }

        private void UpdateCoinsCountView(int value)
        {
            _goldText.text = value.ToString();
        }

        [Inject]
        private void Construct(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        private void OnDisable()
        {
            _playerModel.OnCoinsChanged -= UpdateCoinsCountView;
        }
    }
}