using System.Collections.Generic;
using DM.PR.Common.Entities;
using DM.PR.Data.Repositories;

namespace DM.PR.Business.Providers
{
    internal class KindPhoneProvider : IKindPhoneProvider
    {
        private IKindPhoneRepository _kindPhoneRepository;

        public KindPhoneProvider(IKindPhoneRepository kindPhoneRepository)
        {
            _kindPhoneRepository = kindPhoneRepository;
        }

        public IReadOnlyCollection<KindPhone> GetAll()
        {
            return _kindPhoneRepository.GetAll();
        }
    }
}
