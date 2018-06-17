using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Common.Entities.Account;
using System.Collections.Generic;
using DM.PR.Data.Entity;
using System.Data;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class UserParameterCreater : IParameterCreater<User>, IUserParameterCreator
    {
        public IInputParameter CreateGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectUserById",
                Parameters =
                {
                    {nameof(id), id}
                }
            };
        }

        public IInputParameter CreateGetAll()
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllUsers"
            };
        }

        public IInputParameter CreateSave(User item)
        {
            return new DbInputParameter
            {
                Procedure = "SaveUser",
                Parameters =
                {
                    {nameof(item.Id), item.Id},
                    {nameof(item.EmployeeId), item.EmployeeId},
                    {nameof(item.Login),item.Login},
                    {nameof(item.Password),item.Password},
                    {nameof(item.Roles), item.Roles!=null? ConvertToTable(item.Roles):null }
                }
            };
        }

        public IInputParameter CreateRemove(int id)
        {
            return new DbInputParameter
            {
                Procedure = "DeleteUser",
                Parameters =
                {
                    {nameof(id), id}
                }
            };
        }

        public IInputParameter CreateByLogin(string login)
        {
            return new DbInputParameter
            {
                Procedure = "SelectUserByLogin",
                Parameters =
                {
                    {nameof(login), login}
                }
            };
        }

        public IInputParameter CreateByEmployeeId(int employeeId)
        {
            return new DbInputParameter
            {
                Procedure = "SelectUserByEmployeeId",
                Parameters =
                {
                    {nameof(employeeId), employeeId}
                }
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
