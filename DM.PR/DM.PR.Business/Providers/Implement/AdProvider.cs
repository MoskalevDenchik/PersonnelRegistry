using DM.PR.Common.Helpers;
using DM.PR.Data.Repositories;
using System.Collections.Generic;

namespace DM.PR.Business.Providers.Implement
{
    internal class AdProvider : IAdProvider
    {
        private readonly IAdRepository _adRepository;

        public AdProvider(IAdRepository adRepository)
        {
            Helper.ThrowExceptionIfNull(adRepository);
            _adRepository = adRepository;
        }

        public IReadOnlyCollection<string> GetContent()
        {
            return _adRepository.GetAll();
        }
    }
}
