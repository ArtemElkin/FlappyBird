using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace _Project.Core.Infrastructure.Save
{
    public class PlayerPrefsSaveService : ISaveService
    {
        private const string Path = "save.json";
        
        public void Save(ISave save)
        {
            string json =  JsonConvert.SerializeObject(save);
            PlayerPrefs.SetString(Path, json);
        }

        public T Load<T>() where T : ISave
        {
            if (PlayerPrefs.HasKey(Path))
            {
                string json = PlayerPrefs.GetString(Path);
                T save = JsonConvert.DeserializeObject<T>(json);
                return save;
            }
            return default;
        }
    }
}