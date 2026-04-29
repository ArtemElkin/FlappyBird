using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Core.Tools
{
    public class CustomPool<T> where T : MonoBehaviour
    {
        private T _prefab;
        private List<T> _objects;
        private Transform _path;

        public CustomPool(T prefab, int prewarmObjects, Transform path)
        {
            _prefab = prefab;
            _objects = new List<T>();
            _path = path;

            for (int i = 0; i < prewarmObjects; i++)
            {
                var obj = GameObject.Instantiate(_prefab, _path);
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
            var obj = GameObject.Instantiate(_prefab, _path);
            _objects.Add(obj);
            return obj;
        }
    }
}