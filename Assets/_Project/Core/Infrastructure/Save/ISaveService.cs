namespace _Project.Core.Infrastructure.Save
{
    public interface ISaveService
    {
        void Save(ISave save);
        T Load<T>() where T : ISave;
    }
}