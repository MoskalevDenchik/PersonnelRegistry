using System.Web.Mvc;

namespace DM.PR.WEB.Infrastructure.Bindings
{
    public class ProviderHelper
    {
        private readonly IValueProvider _provider;

        public ProviderHelper(IValueProvider provider)
        {
            _provider = provider;
        }

        public T GetValueOrDefault<T>(string key)
        {
            var result = _provider.GetValue(key);
            return result == null || string.IsNullOrEmpty(result.AttemptedValue) ? default(T) : (T)result.ConvertTo(typeof(T));
        }
    }
}