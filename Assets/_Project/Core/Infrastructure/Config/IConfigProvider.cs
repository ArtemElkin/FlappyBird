using System.Collections.Generic;


namespace _Project.Core.Infrastructure.Config
{
    public interface IConfigProvider
    {
        T GetConfigFromJson<T>(string path) where T : IConfig;
        List<T> GetConfigsFromJson<T>(string path) where T : IConfig;
        T GetConfigFromScriptableObject<T>(string path) where T : IConfig;
        List<T> GetConfigsFromScriptableObjects<T>(string path) where T : IConfig;
    }
}