
namespace DM.PR.Common.Services
{
    public interface IСachingService
    {
        T Get<T>(string key) where T : class;
        bool Add<T>(string key, T value, int seconds);
        void Update<T>(string key, T value, int seconds);
        void DeleteWhoContains(string key);
        void Delete(string key);
    }
}
