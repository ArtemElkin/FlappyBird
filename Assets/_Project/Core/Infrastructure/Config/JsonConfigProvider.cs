using System;
using Newtonsoft.Json;
using UnityEngine;


namespace _Project.Core.Infrastructure.Config
{
    public class JsonConfigProvider : IConfigProvider
    {
        public T GetConfig<T>(string path) where T : IConfig
        {
            TextAsset jsonConfig = Resources.Load<TextAsset>(path);
            if (jsonConfig == null)
            {
                Debug.LogError($"Config file \"{path}\" could not be found."); ;
            }
            T config = JsonConvert.DeserializeObject<T>(jsonConfig.ToString());
            return config;
        }
    }
}