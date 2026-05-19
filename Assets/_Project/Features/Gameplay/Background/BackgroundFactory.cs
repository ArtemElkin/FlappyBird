using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Background
{
    public class BackgroundFactory
    {
        private readonly BackgroundLayerComponent _backgroundLayerPrefab;
        private readonly IInstantiator _instantiator;
        private readonly Transform _parentTransform;


        public BackgroundFactory(IInstantiator instantiator, BackgroundLayerComponent backgroundLayerPrefab, Transform parentTransform)
        {
            _instantiator = instantiator;
            _backgroundLayerPrefab =  backgroundLayerPrefab;
            _parentTransform = parentTransform;
        }

        public LinkedList<BackgroundLayerComponent> CreateGroup(BackgroundLayer layer, int groupId)
        {
            var group = new LinkedList<BackgroundLayerComponent>();
            int count = layer.count;
            var width = layer.sprite.rect.width / layer.sprite.pixelsPerUnit;
            var groupGameObject = _instantiator.CreateEmptyGameObject($"{layer.order}");
            groupGameObject.transform.SetParent(_parentTransform);
            for (int j = 0; j < count; j++)
            {
                var localPosX = j *  width;
                var localPos = new Vector3(localPosX, 0f, 0f);
                var bg = _instantiator.InstantiatePrefabForComponent<BackgroundLayerComponent>(_backgroundLayerPrefab, groupGameObject.transform);
                bg.transform.localPosition = localPos;
                group.AddLast(bg);
                bg.Setup(layer, groupId);
            }
            return group;
        }
    }
}