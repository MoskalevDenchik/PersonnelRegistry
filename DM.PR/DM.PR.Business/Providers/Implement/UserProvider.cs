using DM.PR.Data.SpecificationCreators;
using DM.PR.Common.Entities.Account;
using System.Collections.Generic;
using DM.PR.Data.Specifications;
using DM.PR.Data.Repositories;
using DM.PR.Common.Helpers;
using System.Linq;
using System;

namespace DM.PR.Business.Providers.Implement
{
    internal class UserProvider : IUserProvider
    {
        private readonly IRepository<User> _rep;
        private readonly IUserSpecificationCreator _specificCreator;

        public UserProvider(IRepository<User> rep, IUserSpecificationCreator specificCreator)
        {
            Helper.ThrowExceptionIfNull(rep, specificCreator);
            _rep = rep;
            _specificCreator = specificCreator;
        }

        public User GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Неверный ID");
            }

            return _rep.GetById(id);
        }

        public User GetByLogin(string login)
        {
            ISpecification findByLoginSpecification = _specificCreator.CreateFindByLoginSpecification(login);
            return _rep.FindBy(findByLoginSpecification).FirstOrDefault();
        }

        public IReadOnlyCollection<User> GetAll()
        {
            return _rep.GetAll();
        }
    }
}
