using DM.PR.Common.Entities;
using DM.PR.Common.Enums;
using DM.PR.Data.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DM.PR.Data.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        #region Private

        private IDataBase _dataBase;

        #endregion

        #region Ctor

        public DepartmentRepository(IDataBase dataBase)
        {
            _dataBase = dataBase;
        }

        #endregion

        #region GetById

        public Department GetById(int id)
        {
            return _dataBase.GetEntity(ConvertToDepartment, Procedure.GetDepartmentById, new SqlParameter("@DepartmentId", id));
        }

        #endregion

        #region GetAll

        public IReadOnlyCollection<Department> GetAll()
        {
            var dataSet = _dataBase.GetDataSet(Procedure.GetAllDepartmts);
            if (dataSet != null)
            {
                var departments = dataSet.Tables[0].AsEnumerable();
                var phones = dataSet.Tables[1].AsEnumerable();

                var list = new List<Department>();

                foreach (var item in departments)
                {
                    list.Add(new Department()
                    {
                        Id = (int)item["DepartmentId"],
                        Name = (string)item["Name"],
                        ParentId = item["ParentID"] == DBNull.Value ? null : (int?)item["ParentID"],
                        Address = item["Address"] == DBNull.Value ? null : (string)item["Address"],
                        Description = item["Description"] == DBNull.Value ? null : (string)item["Description"],
                        Phones = FindPhonesById(phones, (int)item["DepartmentId"])
                    });
                }
                return list;
            }
            else
            {
                return null;
            }
        }


        #endregion

        #region Create
        public int Create(Department department)
        {
            SqlParameter[] departmentParameters =
            {
                new SqlParameter("@ParentId",department.ParentId),
                new SqlParameter("@Name", department.Name),
                new SqlParameter("@Address", department.Address),
                new SqlParameter("@Description", department.Description)
            };

            var DepartmentId = _dataBase.GetScalar<int>(Procedure.AddDepartment, departmentParameters);

            if (DepartmentId != 0 && department.Phones != null)
            {
                AddPhones(Convert.ToInt32(DepartmentId), department.Phones.ToArray());
            }

            return DepartmentId;
        }
        #endregion

        #region Delete
        public int Delete(int id)
        {
            return _dataBase.GetResult(Procedure.DeleteDepartment, new SqlParameter("@DepartmentId", id));
        }
        #endregion

        #region Update
        public int Update(Department department)
        {
            SqlParameter[] parameters =
            {
               new SqlParameter("@DepartmentId",department.Id),
               new SqlParameter("@ParentId",department.ParentId),
               new SqlParameter("@Name",department.Name),
               new SqlParameter("@Address",department.Address),
               new SqlParameter("@Description",department.Description),

            };

            var result = _dataBase.GetResult(Procedure.UpdateDepartment, parameters);

            if (department.Phones != null)
            {
                UpdatePhones((int)department.Id, department.Phones.ToArray());
            }

            return result;   //обработать результат
        }
        #endregion


        #region Converters

        public Department ConvertToDepartment(SqlDataReader reader)
        {

            if (reader.HasRows && reader.Read())
            {
                return new Department()
                {
                    Id = (int)reader["DepartmentId"],
                    ParentId = reader["ParentID"] == DBNull.Value ? null : (int?)reader["ParentID"],
                    Name = (string)reader["Name"],
                    Address = reader["Address"] == DBNull.Value ? null : (string)reader["Address"],
                    Description = reader["Description"] == DBNull.Value ? null : (string)reader["Description"],
                    Phones = GetPhone(reader)
                };
            }
            else
            {
                return null;
            }
        }

        private List<Phone> GetPhone(SqlDataReader reader)
        {
            if (reader.NextResult() && reader.HasRows)
            {
                var phones = new List<Phone>();

                while (reader.Read())
                {
                    phones.Add(new Phone()
                    {
                        Id = (int)reader["PhoneId"],
                        Kind = (KindOfPhone)reader["PhoneType"],
                        Number = (string)reader["Phone"]
                    });
                }
                return phones;
            }
            else
            {
                return null;
            }
        }

        private Department ConvertDepartment(SqlDataReader reader)
        {
            return new Department()
            {
                Id = (int)reader["DepartmentId"],
                ParentId = reader["ParentID"] == DBNull.Value ? null : (int?)reader["ParentID"],
                Name = (string)reader["Name"],
                Address = reader["Address"] == DBNull.Value ? null : (string)reader["Address"],
                Description = reader["Description"] == DBNull.Value ? null : (string)reader["Description"]
            };

        }

        private List<Phone> ConvertToPhones(SqlDataReader reader)
        {
            var phones = new List<Phone>();

            while (reader.Read())
            {
                phones.Add(new Phone()
                {
                    Id = (int)reader["PhoneId"],
                    Kind = (KindOfPhone)reader["PhoneType"],
                    Number = (string)reader["Phone"]
                });
            }
            return phones;
        }

        #endregion


        #region Helpers


        public void AddPhones(int departmentId, params Phone[] phone)
        {
            foreach (var item in phone)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@DepartmetnId",departmentId),
                    new SqlParameter("@Phone",item.Number),
                    new SqlParameter("@PhoneType",item.Kind)
                };
                var result = _dataBase.GetResult(Procedure.AddPhoneByDepartmentId, parameters);
            }
        }


        public void UpdatePhones(int departmentId, params Phone[] phone)
        {
            foreach (var item in phone)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@DepartmentId",departmentId),
                    new SqlParameter("@PhoneId",item.Id),
                    new SqlParameter("@Number",item.Number),
                    new SqlParameter("@PhoneType",item.Kind)
                };
                var result = _dataBase.GetResult(Procedure.UpdateDepartmentPhone, parameters);
            }
        }


        private List<Phone> FindPhonesById(EnumerableRowCollection<DataRow> phones, int id)
        {
            var listOfPhones = from phone in phones
                               where phone.Field<int>("DepartmentId") == id
                               select new { Number = phone.Field<string>("Phone"), Kind = phone.Field<int>("PhoneType") };

            if (listOfPhones != null)
            {
                List<Phone> list = new List<Phone>();
                foreach (var item in listOfPhones)
                {
                    list.Add(new Phone() { Number = item.Number, Kind = (KindOfPhone)item.Kind });
                }

                return list;
            }
            else return null;
        }

        #endregion

    }
}

