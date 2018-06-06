using DM.PR.Common.Entities.Account;
using DM.PR.Data.Repositories;
using DM.PR.Common.Helpers;
using System;

namespace DM.PR.Business.Services.Implement
{
    internal class UserService : IUserService
    {
        private readonly IRepository<User> _rep;

        public UserService(IRepository<User> rep)
        {
            Helper.ThrowExceptionIfNull(rep);
            _rep = rep;
        }

        public void Create(User user)
        {
            _rep.Add(user);
        }

        public void Edit(User user)
        {
            _rep.Update(user);
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Неверный ID");
            }
            _rep.Remove(id);
        }
    }
}
