﻿using System.Collections.Generic;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;
using System.Data;

namespace DM.PR.Data.Core.InputParameters.Creaters.Implement
{
    internal class DepartmentParameterCreater : IParameterCreater<Department>, IDepartmentParameterCreater
    {
        public IInputParameter CreateGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectDepartmentById",
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
                Procedure = "SelectAllDepartments"
            };
        }

        public IInputParameter CreateSave(Department item)
        {
            return new DbInputParameter
            {
                Procedure = "SaveDepartment",
                Parameters =
                {   {nameof(item.Id), item.Id },
                    {nameof(item.Name), item.Name},
                    {nameof(item.Address), item.Address },
                    {nameof(item.ParentId), item.ParentId },
                    {nameof(item.Description), item.Description},
                    {nameof(item.Phones), item.Phones!=null? ConvertToTable(item.Phones):null}
                }
            };
        }

        public IInputParameter CreateRemove(int id)
        {
            return new DbInputParameter
            {
                Procedure = "DeleteDepartment",
                Parameters =
                {
                    {nameof(id), id}
                }
            };
        }

        public IInputParameter CreateFind(int pageSize, int pageNumber)
        {
            return new DbInputParameter
            {
                Procedure = "SelectPageDepartmts",
                Parameters =
                {
                    {nameof(pageSize), pageSize},
                    {nameof(pageNumber), pageNumber}
                }
            };
        }

        public IInputParameter CreateFind(int parentId)
        {
            return new DbInputParameter
            {
                Procedure = "SelectDepartmentByParentId",
                Parameters =
                {
                    {nameof(parentId), parentId}
                }
            };
        }

        #region Converters

        private static DataTable ConvertToTable(IReadOnlyCollection<Phone> phones)
        {
            var table = new DataTable("Phones");
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Number", typeof(string));

            foreach (var item in phones)
            {
                table.Rows.Add(item.Id, item.Number);
            }
            return table;
        }

        #endregion
    }
}
