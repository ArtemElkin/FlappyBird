namespace _Project.Core.Infrastructure.Config
{
    public interface IConfigProvider
    {
        T GetConfig<T>(string path);
        
    }
}