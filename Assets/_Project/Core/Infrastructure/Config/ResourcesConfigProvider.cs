using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Object = UnityEngine.Object;


namespace _Project.Core.Infrastructure.Config
{
    public class ResourcesConfigProvider : IConfigProvider
    {
        public T GetConfigFromJson<T>(string path) where T : IConfig
        {
            TextAsset jsonConfig = Resources.Load<TextAsset>(path);
            if (jsonConfig == null)
            {
                Debug.LogError($"Config file \"{path}\" could not be found."); ;
            }
            T config = JsonConvert.DeserializeObject<T>(jsonConfig.ToString());
            return config;
        }

        public List<T> GetConfigsFromJson<T>(string path) where T : IConfig
        {
            throw new NotImplementedException();
        }

        public T GetConfigFromScriptableObject<T>(string path) where T : IConfig
        {
            Object asset = Resources.Load(path);

            if (asset == null)
            {
                return default;
            }

            if (asset is T config)
            {
                return config;
            }
            return default;
        }

        public List<T> GetConfigsFromScriptableObjects<T>(string path) where T : IConfig
        {
            var assets = Resources.LoadAll(path);
            var configs = new List<T>();
            foreach (var asset in assets)
            {
                if (asset is T config)
                {
                    configs.Add(config);
                }
            }
            return configs;
        }
    }
}