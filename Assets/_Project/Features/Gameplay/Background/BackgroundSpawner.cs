using System;
using System.Collections.Generic;
using _Project.Core.Infrastructure.Config;
using _Project.Core.Player;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Chunk;
using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Background
{
    public class BackgroundSpawner : IInitializable, IDisposable
    {
        private List<LinkedList<BackgroundLayerComponent>> _backgroundGroups;
        private List<BackgroundLayer> _backgroundLayers;
        private ChunkConfig _chunkConfig;
        private readonly BackgroundLayerComponent _backgroundLayerPrefab;
        private readonly Transform _parentTransform;
        private readonly BackgroundWarper _backgroundWarper;
        private readonly PlayerModel _playerModel;
        private readonly IConfigProvider _configProvider;
        private readonly IInstantiator _instantiator;
        private readonly SignalBus _signalBus;
        private readonly ScreenBoundsCalculator _screenBoundsCalculator;

        
        public BackgroundSpawner(
            IConfigProvider configProvider,
            IInstantiator instantiator,
            SignalBus signalBus,
            BackgroundLayerComponent backgroundLayerPrefab,
            Transform parentTransform,
            BackgroundWarper backgroundWarper,
            PlayerModel playerModel,
            ScreenBoundsCalculator screenBoundsCalculator)
        {
            _configProvider = configProvider;
            _instantiator = instantiator;
            _signalBus = signalBus;
            _backgroundLayerPrefab =  backgroundLayerPrefab;
            _parentTransform = parentTransform;
            _backgroundWarper = backgroundWarper;
            _playerModel = playerModel;
            _screenBoundsCalculator = screenBoundsCalculator;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<BackgroundInWarpZoneSignal>(OnBackgroundInWarpZone);
            var backgroundConfig = _configProvider.GetConfigFromScriptableObject<BackgroundConfig>($"Backgrounds/Background{_playerModel.CurrentBackgroundId}");
            _backgroundLayers = backgroundConfig.backgroundLayers;
            _backgroundGroups = new List<LinkedList<BackgroundLayerComponent>>();
            SpawnBackground();
            var firstBackgrounds = new List<BackgroundLayerComponent>();
            foreach (var group in _backgroundGroups)
            {
                firstBackgrounds.Add(group.First.Value);
            }
            
            var backgroundWidth = _backgroundLayers[0].sprite.rect.width / _backgroundLayers[0].sprite.pixelsPerUnit;
            var warpPosX = _screenBoundsCalculator.LeftEdgeX - backgroundWidth;
            
            _backgroundWarper.Setup(firstBackgrounds, warpPosX);
        }

        private void SpawnBackground()
        {
            for (int i = 0; i < _backgroundLayers.Count; i++)
            {
                var group = new LinkedList<BackgroundLayerComponent>();
                int count = _backgroundLayers[i].count;
                var width = _backgroundLayers[i].sprite.rect.width / _backgroundLayers[i].sprite.pixelsPerUnit;
                var groupGameObject = _instantiator.CreateEmptyGameObject($"{_backgroundLayers[i].order}");
                groupGameObject.transform.SetParent(_parentTransform);
                for (int j = 0; j < count; j++)
                {
                    var localPosX = j *  width;
                    var localPos = new Vector3(localPosX, 0f, 0f);
                    var bg = _instantiator.InstantiatePrefabForComponent<BackgroundLayerComponent>(_backgroundLayerPrefab, groupGameObject.transform);
                    bg.transform.localPosition = localPos;
                    bg.Setup(_backgroundLayers[i]);
                    group.AddLast(bg);
                }
                _backgroundGroups.Add(group);
            }
        }

        private void OnBackgroundInWarpZone(BackgroundInWarpZoneSignal signal)
        {
            var backgroundToWarp = signal.BackgroundLayerToWarp;
            LinkedList<BackgroundLayerComponent> backgroundGroup = new LinkedList<BackgroundLayerComponent>();
            foreach (var group in _backgroundGroups)
            {
                if (group.Contains(backgroundToWarp))
                {
                    backgroundGroup = group;
                    break;
                }
            }
            var width = backgroundToWarp.backgroundLayer.sprite.rect.width / backgroundToWarp.backgroundLayer.sprite.pixelsPerUnit;
            var lastNode = backgroundGroup.Last;
            float lastX = lastNode.Value.transform.localPosition.x;
            float targetX = lastX + width;
            backgroundToWarp.transform.localPosition = new Vector3(targetX, 0f, 0f);
            backgroundGroup.RemoveFirst();
            backgroundGroup.AddLast(backgroundToWarp);
            _signalBus.Fire<FirstBackgroundChangedSignal>(new FirstBackgroundChangedSignal(backgroundGroup.First.Value, backgroundToWarp));
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<BackgroundInWarpZoneSignal>(OnBackgroundInWarpZone);
        }
    }
}