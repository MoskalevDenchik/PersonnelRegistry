using DM.PR.Data.Repositories;
using System.Collections.Generic;

namespace DM.PR.Business.Providers.Implement
{
    public class AdProvider : IAdProvider
    {
        #region Private

        private IAdRepository _adRepository;

        #endregion

        #region Ctor

        public AdProvider(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        #endregion

        #region GetContent

        public IReadOnlyCollection<string> GetContent()
        {
            return _adRepository.GetAll();
        }

        #endregion

    }
}
