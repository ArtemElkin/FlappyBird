using _Project.Core.Signals;
using _Project.Core.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Features.UI.MainMenu
{
    public class PlayButton : MonoBehaviour
    {
        private Button _button;
        private GameStateMachine _gameStateMachine;
        private SignalBus _signalBus;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        [Inject]
        public void Construct(GameStateMachine gameStateMachine, SignalBus signalBus)
        {
            _gameStateMachine = gameStateMachine;
            _signalBus = signalBus;
        }

        private void OnButtonClick()
        {
            _gameStateMachine.EnterState<LoadLevelState>();
            _signalBus.Fire<GameStartedSignal>();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
