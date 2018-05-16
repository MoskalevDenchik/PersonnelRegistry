using DM.PR.Data.Repositories;
using System.Collections.Generic;
using System;

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
            var list = _adRepository.GetAll();
            if (list != null)
            {   
                return list;
            }
            else return null;
        }
        #endregion

    }
}
