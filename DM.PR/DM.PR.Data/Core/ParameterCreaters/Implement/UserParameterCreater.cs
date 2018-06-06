using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Common.Entities.Account;
using DM.PR.Data.Specifications;
using System.Data.SqlClient;
using DM.PR.Data.Entity;
using System;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class UserParameterCreater : IParameterCreater<User> , IUserParameterCreator
    {
        public IInputParameter CreateForGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectUserById",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", id)
                }
            };
        }

        public IInputParameter CreateForGetAll()
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllUser",
                Parameters = null
            };
        }

        public IInputParameter CreateForFindBy(ISpecification specification)
        {
            return specification.GetSpecific();
        }

        public IInputParameter CreateForAdd(User item)
        {
            throw new NotImplementedException();
        }

        public IInputParameter CreateForUpdate(User item)
        {
            throw new NotImplementedException();
        }

        public IInputParameter CreateForRemove(int id)
        {
            throw new NotImplementedException();
        }

        public IInputParameter CreateForFindByLogin(string login)
        {
            return new DbInputParameter
            {
                Procedure = "SelectUserByLogin",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@Login", login)
                }
            };
        }
    }
}
