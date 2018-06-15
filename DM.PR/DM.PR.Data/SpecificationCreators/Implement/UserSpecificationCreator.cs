using DM.PR.Data.Core.ParameterCreaters;
using DM.PR.Data.Specifications;
using DM.PR.Common.Helpers;
using DM.PR.Data.Entities;

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

        public ISpecification CreateSpecification(string login)
        {
            return new Specification(_paramCreator.CreateForFindByLogin(login));
        }

        public ISpecification CreateSpecification(int employeeId)
        {
            return new Specification(_paramCreator.CreateForFindByEmployeeId(employeeId));
        }
    }
}
