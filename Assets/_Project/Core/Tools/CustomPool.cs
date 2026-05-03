using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;


namespace _Project.Core.Tools
{
    public class CustomPool<T> where T : MonoBehaviour
    {
        private T _prefab;
        private List<T> _objects;
        private Transform _path;
        private IInstantiator _instantiator;

        public CustomPool(IInstantiator instantiator, T prefab, int prewarmObjects, Transform path)
        {
            _instantiator = instantiator;
            _prefab = prefab;
            _objects = new List<T>();
            _path = path;

            for (int i = 0; i < prewarmObjects; i++)
            {
                var obj = _instantiator.InstantiatePrefabForComponent<T>(_prefab, _path);
                obj.gameObject.SetActive(false);
                _objects.Add(obj);
            }
        }

        public T Get()
        {
            var obj = _objects.FirstOrDefault(x => !x.isActiveAndEnabled);

            if (obj == null)
            {
                obj = Create();
            }

            obj.gameObject.SetActive(true);
            return obj;
        }

        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        private T Create()
        {
            var obj = _instantiator.InstantiatePrefabForComponent<T>(_prefab, _path);
            _objects.Add(obj);
            return obj;
        }
    }
}