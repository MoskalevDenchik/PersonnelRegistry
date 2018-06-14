using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Common.Entities.Account;
using System.Collections.Generic;
using DM.PR.Data.Specifications;
using System.Data.SqlClient;
using DM.PR.Data.Entity;
using System.Data;
using System;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class UserParameterCreater : IParameterCreater<User>, IUserParameterCreator
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
                Procedure = "SelectAllUsers",
                Parameters = null
            };
        }

        public IInputParameter CreateForFindBy(ISpecification specification)
        {
            return specification.GetSpecific();
        }

        public IInputParameter CreateForAdd(User item)
        {
            return new DbInputParameter
            {
                Procedure = "InsertUser",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeId", item.Id),
                    new SqlParameter("@Login",item.Login),
                    new SqlParameter("@Password",item.Password),
                    new SqlParameter("@Roles", item.Roles!=null? ConvertToTable(item.Roles):null)
                }
            };
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

        #region Converters

        private static DataTable ConvertToTable(IReadOnlyCollection<Role> users)
        {
            var table = new DataTable("Users");
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));

            foreach (var item in users)
            {
                table.Rows.Add(item.Id, item.Name);
            }
            return table;
        }

        #endregion
    }
}
