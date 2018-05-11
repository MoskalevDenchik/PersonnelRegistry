using DM.PR.Common.Entities;
using DM.PR.Common.Enums;
using DM.PR.Data.Intefaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DM.PR.Data.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        #region Get
        public Department Get(int? id)
        {
            Department department = new Department();
            List<Phone> phones = new List<Phone>();

            SqlParameter[] parameters =
            {
                new SqlParameter("@DepartmentId",id)
            };


            var reader = DataBase.ExecuteReader("GetDepartmentById", parameters);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    department.Id = (int)reader["DepartmentId"];
                    department.Name = (string)reader["Name"];
                    department.ParentId = reader["ParentID"] == DBNull.Value ? null : (int?)reader["ParentID"];
                    department.Address = reader["Address"] == DBNull.Value ? null : (string)reader["Address"];
                    department.Description = reader["Description"] == DBNull.Value ? null : (string)reader["Description"];
                }
            }

            reader.NextResult();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    phones.Add(new Phone()
                    {
                        Kind = (KindOfPhone)reader["PhoneType"],
                        Number = (string)reader["Phone"]
                    });
                }
            }

            department.Phones = phones;

            DataBase.CloseConnection();

            return department;

        }
        #endregion
         
        #region GetAll
        public IReadOnlyCollection<Department> GetAll()
        {
            var list = new List<Department>();

            var reader = DataBase.ExecuteReader("GetAllDepartmts", null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    list.Add(new Department()
                    {
                        Id = (int)reader["DepartmentId"],
                        Name = (string)reader["Name"],
                        ParentId = reader["ParentID"] == DBNull.Value ? null : (int?)reader["ParentID"],
                        Address = reader["Address"] == DBNull.Value ? null : (string)reader["Address"],
                        Description = reader["Description"] == DBNull.Value ? null : (string)reader["Description"]
                    });
                }
            }
            DataBase.CloseConnection();

            return list;
        }
        #endregion

        #region GetAllAsNavModel
        public IReadOnlyCollection<DepartmentNavModel> GetAllAsNavModel()
        {
            var list = new List<DepartmentNavModel>();


            var reader = DataBase.ExecuteReader("GetAllDepartmentNav", null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    list.Add(new DepartmentNavModel()
                    {
                        Id = (int)reader["DepartmentId"],
                        Name = (string)reader["Name"],
                        ParentId = reader["ParentID"] == DBNull.Value ? null : (int?)reader["ParentID"]
                    });
                }
            }

            DataBase.CloseConnection();
            return AddChildren(list).Where(x => x.ParentId == null).ToList();
        }
        #endregion

        #region Create
        public void Create(Department department)
        {
            SqlParameter[] departmentParameters =
            {
                new SqlParameter("@ParentId",department.ParentId),
                new SqlParameter("@Name", department.Name),
                new SqlParameter("@Address", department.Address),
                new SqlParameter("@Description", department.Description)
            };

            var DepartmentId = DataBase.ExecuteScalar("AddDepartment", departmentParameters);

            if (department.Phones != null)
            {
                foreach (var item in department.Phones)
                {
                    SqlParameter[] phoneParameters =
                    {
                    new SqlParameter("@DepartmetnId",DepartmentId),
                    new SqlParameter("@Phone",item.Number),
                    new SqlParameter("@PhoneType",item.Kind)
                };

                    DataBase.ExecuteNonQuery("AddPhoneByDepartmentId", phoneParameters);
                }
            }

            DataBase.CloseConnection();
        }
        #endregion

        #region Delete
        public void Delete(int? id)
        {
            SqlParameter[] parametrs =
            {
                new SqlParameter("@DepartmentId",id)
            };

            DataBase.ExecuteNonQuery("DeleteDepartmentById", parametrs);
            DataBase.CloseConnection();
        }
        #endregion

        #region Update
        public void Update(Department department)
        {
            SqlParameter[] parameters =
            {
               new SqlParameter("@DepartmentId",department.Id),
               new SqlParameter("@ParentId",department.ParentId),
               new SqlParameter("@Name",department.Name),
               new SqlParameter("@Address",department.Address),
               new SqlParameter("@Description",department.Description), 

            };

            DataBase.ExecuteNonQuery("UpdateDepartment", parameters);
            DataBase.CloseConnection();
        }
        #endregion

        #region Helpers
        IReadOnlyCollection<DepartmentNavModel> AddChildren(IReadOnlyCollection<DepartmentNavModel> list)
        {
            foreach (var item in list)
            {
                var childrenList = list.Where(x => x.ParentId == item.Id).ToList();
                item.Cildren = childrenList != null ? AddChildren(childrenList) : null;
            }
            return list;
        }
        #endregion
    }
}
