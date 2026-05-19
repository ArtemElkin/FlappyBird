using System.Collections.Generic;
using UnityEngine;


namespace _Project.Core.Infrastructure.Config
{
    public interface IConfigProvider
    {
        T GetConfigFromJson<T>(string path) where T : IConfig;
        T GetConfigFromScriptableObject<T>(string path) where T : ScriptableObject, IConfig;
        List<T> GetConfigsFromScriptableObjects<T>(string path) where T : ScriptableObject, IConfig;
    }
}