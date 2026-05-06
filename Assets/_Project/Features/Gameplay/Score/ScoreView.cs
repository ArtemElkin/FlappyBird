using _Project.Core.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Features.Gameplay.Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        private PlayerModel _playerModel;

        private void Awake()
        {
            _scoreText = GetComponent<TextMeshProUGUI>();
            UpdateScoreView(_playerModel.CurrentScore);
        }

        private void OnEnable()
        {
            _playerModel.OnCurrentScoreChanged += UpdateScoreView;
        }

        [Inject]
        private void Construct(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        private void UpdateScoreView(int value)
        {
            _scoreText.text = value.ToString();
        }
        
        private void OnDisable()
        {
            _playerModel.OnCurrentScoreChanged -= UpdateScoreView;
        }
    }
}