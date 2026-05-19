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
        private Dictionary<int, LinkedList<BackgroundLayerComponent>> _backgroundGroups;
        private List<BackgroundLayer> _backgroundLayers;
        private ChunkConfig _chunkConfig;
        private readonly BackgroundWarper _backgroundWarper;
        private readonly PlayerModel _playerModel;
        private readonly IConfigProvider _configProvider;
        private readonly SignalBus _signalBus;
        private readonly ScreenBoundsCalculator _screenBoundsCalculator;
        private readonly BackgroundFactory _backgroundFactory;

        
        public BackgroundSpawner(
            IConfigProvider configProvider,
            SignalBus signalBus,
            BackgroundWarper backgroundWarper,
            PlayerModel playerModel,
            ScreenBoundsCalculator screenBoundsCalculator,
            BackgroundFactory backgroundFactory)
        {
            _configProvider = configProvider;
            _signalBus = signalBus;
            _backgroundWarper = backgroundWarper;
            _playerModel = playerModel;
            _screenBoundsCalculator = screenBoundsCalculator;
            _backgroundFactory = backgroundFactory;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<BackgroundInWarpZoneSignal>(OnBackgroundInWarpZone);
            
            SetupSpawner();
            SpawnBackground();
            SetupWarper();
        }

        private void SetupSpawner()
        {
            var backgroundConfig = _configProvider.GetConfigFromScriptableObject<BackgroundConfig>($"Backgrounds/Background{_playerModel.CurrentBackgroundId}");
            _backgroundLayers = backgroundConfig.backgroundLayers;
            _backgroundGroups = new Dictionary<int, LinkedList<BackgroundLayerComponent>>();
        }

        private void SetupWarper()
        {
            var firstBackgrounds = new List<BackgroundLayerComponent>();
            foreach (var group in _backgroundGroups.Values)
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
                var groupId = i;
                var group = _backgroundFactory.CreateGroup(_backgroundLayers[i], groupId);
                _backgroundGroups[groupId] = group;
            }
        }

        private void OnBackgroundInWarpZone(BackgroundInWarpZoneSignal signal)
        {
            var backgroundToWarp = signal.BackgroundLayerToWarp;
            var groupId = backgroundToWarp.GroupId;
            var group = _backgroundGroups[groupId];
            
            var width = backgroundToWarp.Layer.sprite.rect.width / backgroundToWarp.Layer.sprite.pixelsPerUnit;
            var lastNode = group.Last;
            float lastX = lastNode.Value.transform.localPosition.x;
            float targetX = lastX + width;
            backgroundToWarp.transform.localPosition = new Vector3(targetX, 0f, 0f);
            group.RemoveFirst();
            group.AddLast(backgroundToWarp);
            _signalBus.Fire<FirstBackgroundChangedSignal>(new FirstBackgroundChangedSignal(group.First.Value, backgroundToWarp));
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<BackgroundInWarpZoneSignal>(OnBackgroundInWarpZone);
        }
    }
}