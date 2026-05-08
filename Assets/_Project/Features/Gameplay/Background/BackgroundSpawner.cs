using System;
using System.Collections.Generic;
using _Project.Core.Infrastructure.Config;
using _Project.Features.Gameplay.Chunk;
using _Project.Features.Gameplay.Signals;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Background
{
    public class BackgroundSpawner : IInitializable, IDisposable
    {
        private List<LinkedList<BackgroundComponent>> _backgroundGroups;
        private readonly List<Background> _backgroundConfigs;
        private readonly BackgroundComponent _backgroundPrefab;
        private readonly Transform _parentTransform;
        private readonly IConfigProvider _configProvider;
        private readonly SignalBus _signalBus;
        private readonly IInstantiator _instantiator;
        private ChunkConfig _chunkConfig;
        private readonly BackgroundWarper _backgroundWarper;

        public BackgroundSpawner(
            IConfigProvider configProvider,
            IInstantiator instantiator,
            SignalBus signalBus,
            List<Background> backgroundConfigs,
            BackgroundComponent backgroundPrefab,
            Transform parentTransform,
            BackgroundWarper backgroundWarper)
        {
            _configProvider = configProvider;
            _instantiator = instantiator;
            _signalBus = signalBus;
            _backgroundConfigs = backgroundConfigs;
            _backgroundPrefab =  backgroundPrefab;
            _parentTransform = parentTransform;
            _backgroundWarper = backgroundWarper;
        }
        public void Initialize()
        {
            _signalBus.Subscribe<BackgroundInWarpZoneSignal>(OnBackgroundInWarpZone);
            var config = _configProvider.GetConfig<ChunkConfig>("ChunkConfig");
            _backgroundGroups = new List<LinkedList<BackgroundComponent>>();
            SpawnBackground();
            var firstBackgrounds = new List<BackgroundComponent>();
            foreach (var group in _backgroundGroups)
            {
                firstBackgrounds.Add(group.First.Value);
            }
            _backgroundWarper.Setup(firstBackgrounds);
        }

        public void SpawnBackground()
        {
            for (int i = 0; i < _backgroundConfigs.Count; i++)
            {
                var group = new LinkedList<BackgroundComponent>();
                int count = _backgroundConfigs[i].count;
                var width = _backgroundConfigs[i].sprite.rect.width / _backgroundConfigs[i].sprite.pixelsPerUnit;
                var groupGameObject = _instantiator.CreateEmptyGameObject($"{_backgroundConfigs[i].order}");
                groupGameObject.transform.SetParent(_parentTransform);
                for (int j = 0; j < count; j++)
                {
                    var localPosX = j *  width;
                    var localPos = new Vector3(localPosX, 0f, 0f);
                    var bg = _instantiator.InstantiatePrefabForComponent<BackgroundComponent>(_backgroundPrefab, groupGameObject.transform);
                    bg.transform.localPosition = localPos;
                    bg.Setup(_backgroundConfigs[i]);
                    group.AddLast(bg);
                }
                _backgroundGroups.Add(group);
            }
        }

        private void OnBackgroundInWarpZone(BackgroundInWarpZoneSignal signal)
        {
            var background = signal.backgroundToWarp;
            LinkedList<BackgroundComponent> backgroundGroup = new LinkedList<BackgroundComponent>();
            foreach (var group in _backgroundGroups)
            {
                if (group.Contains(background))
                {
                    backgroundGroup = group;
                    break;
                }
            }
            var width = background.background.sprite.rect.width / background.background.sprite.pixelsPerUnit;
            var lastNode = backgroundGroup.Last;
            float lastX = lastNode.Value.transform.localPosition.x;
            float targetX = lastX + width;
            background.transform.localPosition = new Vector3(targetX, 0f, 0f);
            backgroundGroup.RemoveFirst();
            backgroundGroup.AddLast(background);
            _signalBus.Fire<FirstBackgroundChangedSignal>(new FirstBackgroundChangedSignal(backgroundGroup.First.Value, background));
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<BackgroundInWarpZoneSignal>(OnBackgroundInWarpZone);
        }
    }
}