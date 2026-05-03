using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace _Project.Core.Infrastructure.Save
{
    public class JsonSaveService : ISaveService
    {
        private readonly string _path = "save.json";
        
        public void Save(ISave save)
        {
            throw new System.NotImplementedException();
        }

        public T Load<T>() where T : ISave
        {
            if (PlayerPrefs.HasKey(_path))
            {
                string json = PlayerPrefs.GetString(_path);
                T save = JsonConvert.DeserializeObject<T>(json);
                return save;
            }
            return default;
        }
    }
}