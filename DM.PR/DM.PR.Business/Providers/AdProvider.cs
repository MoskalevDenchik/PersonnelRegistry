using DM.PR.Business.Interfaces;
using DM.PR.Data.Intefaces;
using System.Collections.Generic;

namespace DM.PR.Business.Providers
{
    public class AdProvider : IAdProvider
    {
        private IAdRepository _adRepository;

        public AdProvider(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        public IEnumerable<string> GetContent()
        {
            return _adRepository.GetAll();
        }
    }
}
