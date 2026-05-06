using _Project.Core.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Features.UI.Gameplay.Pause
{
    public class RestartButton : MonoBehaviour
    {
        private Button _button;
        private GameStateMachine _gameStateMachine;

        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }
        
        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void OnButtonClick()
        {
            _gameStateMachine.EnterState<GameplayState>();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }
        
    }
}