using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;


namespace _Project.Core.Tools
{
    public class CustomPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly List<T> _objects;
        private readonly Transform _defaultParentTransform;
        private readonly IInstantiator _instantiator;

        
        public CustomPool(IInstantiator instantiator, T prefab, int prewarmObjects = 0, Transform defaultParentTransform = null)
        {
            _instantiator = instantiator;
            _prefab = prefab;
            _objects = new List<T>();
            
            _defaultParentTransform = defaultParentTransform;
            
            for (int i = 0; i < prewarmObjects; i++)
            {
                var obj = _instantiator.InstantiatePrefabForComponent<T>(_prefab);
                obj.gameObject.SetActive(false);
                if (_defaultParentTransform != null)
                {
                    obj.transform.SetParent(_defaultParentTransform);
                }
                _objects.Add(obj);
            }
        }

        public T Get(Transform parentTransform = null)
        {
            if (parentTransform == null)
                parentTransform = _defaultParentTransform;
            
            var obj = _objects.FirstOrDefault(x => !x.gameObject.activeSelf);

            if (obj == null)
            {
                obj = Create();
            }

            obj.gameObject.SetActive(true);
            if (parentTransform != null)
            {
                obj.transform.SetParent(parentTransform);
            }
            return obj;
        }

        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
            if (_defaultParentTransform != null)
            {
                obj.transform.SetParent(_defaultParentTransform);
            }
        }

        private T Create()
        {
            var obj = _instantiator.InstantiatePrefabForComponent<T>(_prefab);
            _objects.Add(obj);
            return obj;
        }
    }
}