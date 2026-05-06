using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace _Project.Core.Infrastructure.Save
{
    public class PlayerPrefsSaveService : ISaveService
    {
        private readonly string _path = "save.json";
        
        public void Save(ISave save)
        {
            string json =  JsonConvert.SerializeObject(save);
            PlayerPrefs.SetString(_path, json);
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