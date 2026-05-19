using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;


namespace _Project.Core.Infrastructure.Config
{
    public class ResourcesConfigProvider : IConfigProvider
    {
        public T GetConfigFromJson<T>(string path) where T : IConfig
        {
            TextAsset jsonConfig = Resources.Load<TextAsset>(path);
            if (jsonConfig == null)
            {
                Debug.LogError($"Config file \"{path}\" could not be found.");
                return default;
            }
            try
            {
                T config = JsonConvert.DeserializeObject<T>(jsonConfig.text);
                return config;
            }
            catch (Exception e)
            {
                Debug.LogError($"Config file \"{path}\" could not be deserialized.");
                return default;
            }
        }

        public T GetConfigFromScriptableObject<T>(string path) where T : ScriptableObject, IConfig
        {
            var config = Resources.Load<T>(path);
            if (config == null)
            {
                Debug.LogError($"Config file \"{path}\" could not be found.");
                return null;
            }
            return config;
        }

        public List<T> GetConfigsFromScriptableObjects<T>(string path) where T : ScriptableObject, IConfig
        {
            var configs = Resources.LoadAll<T>(path);
            if (configs.Length == 0)
            {
                Debug.LogError($"Config files could not be found in \"{path}\"");
            }
            return new List<T>(configs);
        }
    }
}