using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Common.Entities.Account;
using System.Collections.Generic;
using DM.PR.Data.Entity;
using System.Data;
using System;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class UserParameterCreater : IParameterCreater<User>, IUserParameterCreator
    {
        public override IInputParameter CreateForGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectUserById",
                Parameters = { { "@Id", id } }
            };
        }

        public override IInputParameter CreateForGetAll()
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllUsers",
                Parameters = null
            };
        }
         

        public override IInputParameter CreateForAdd(User item)
        {
            return new DbInputParameter
            {
                Procedure = "InsertUser",
                Parameters =
                {
                    {"@EmployeeId", item.EmployeeId},
                    {"@Login",item.Login},
                    {"@Password",item.Password},
                    {"@Roles", item.Roles!=null? ConvertToTable(item.Roles):null }
                }
            };
        }

        public override IInputParameter CreateForUpdate(User item)
        {
            throw new NotImplementedException();
        }

        public override IInputParameter CreateForRemove(int id)
        {
            return new DbInputParameter
            {
                Procedure = "DeleteUser",
                Parameters = { { "@Id", id } }
            };
        }

        public IInputParameter CreateForFindByLogin(string login)
        {
            return new DbInputParameter
            {
                Procedure = "SelectUserByLogin",
                Parameters = { { "@Login", login } }
            };
        }

        public IInputParameter CreateForFindByEmployeeId(int employeeId)
        {
            return new DbInputParameter
            {
                Procedure = "SelectUserByEmployeeId",
                Parameters = { { "@Id", employeeId } }
            };
        }

        #region Converters

        private DataTable ConvertToTable(IReadOnlyCollection<Role> users)
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
