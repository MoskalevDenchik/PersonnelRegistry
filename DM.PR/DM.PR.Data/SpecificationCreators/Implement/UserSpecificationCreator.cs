using DM.PR.Data.Core.ParameterCreaters;
using DM.PR.Data.Specifications;
using DM.PR.Data.Entities;
using DM.PR.Common.Helpers;

namespace DM.PR.Data.SpecificationCreators.Implement
{
    internal class UserSpecificationCreator : IUserSpecificationCreator
    {
        private IUserParameterCreator _paramCreator;

        public UserSpecificationCreator(IUserParameterCreator paramCreator)
        {
            Inspector.ThrowExceptionIfNull(paramCreator);
            _paramCreator = paramCreator;
        }

        public ISpecification CreateFindByLoginSpecification(string login)
        {
            return new Specification(_paramCreator.CreateForFindByLogin(login));
        }
    }
}
