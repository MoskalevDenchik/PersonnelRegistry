using DM.PR.Common.Entities;
using DM.PR.Common.Enums;
using DM.PR.Data.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DM.PR.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        #region Private

        private IDataBase _dataBase;

        #endregion

        #region Ctor

        public EmployeeRepository(IDataBase dataBase)
        {
            _dataBase = dataBase;
        }

        #endregion

        #region GetAll

        public IReadOnlyCollection<Employee> GetAll()
        {
            var dataSet = _dataBase.GetDataSet(Procedure.GetAllEmployees);

            if (dataSet != null)
            {
                var employees = dataSet.Tables[0].AsEnumerable();
                var phones = dataSet.Tables[1].AsEnumerable();
                var emails = dataSet.Tables[2].AsEnumerable();

                var list = new List<Employee>();

                foreach (var item in employees)
                {
                    list.Add(new Employee()
                    {
                        Id = (int)item["EmployeeId"],
                        FirstName = item["FirstName"] == DBNull.Value ? null : (string)item["FirstName"],
                        MiddleName = item["MiddleName"] == DBNull.Value ? null : (string)item["MiddleName"],
                        LastName = item["LastName"] == DBNull.Value ? null : (string)item["LastName"],
                        DepartmentId = item["DepartmentId"] == DBNull.Value ? null : (int?)item["DepartmentId"],
                        ImagePath = item["ImagePath"] == DBNull.Value ? null : (string)item["ImagePath"],
                        BeginningOfWork = item["BeginningOfWork"] == DBNull.Value ? null : (DateTime?)item["BeginningOfWork"],
                        EndOfWork = item["EndOfWork"] == DBNull.Value ? null : (DateTime?)item["EndOfWork"],
                        Address = item["Address"] == DBNull.Value ? null : (string)item["Address"],
                        Phones = FindPhonesById(phones, (int)item["EmployeeId"]),
                        Emails = FindEmailsById(emails, (int)item["EmployeeId"])
                    });
                }
                return list;
            }
            else return null;
        }

        #endregion

        #region GetAllByDepartmentId

        public IReadOnlyCollection<Employee> GetAllByDepartmentId(int id)
        {
            var reader = _dataBase.GetReader(Procedure.GetEmployeesByDepartmentId, "@DepartmentId", id);

            if (reader != null)
            {
                var employees = new List<Employee>();

                while (reader.Read())
                {
                    employees.Add(ConvertToEmployee(reader));
                }

                _dataBase.CloseReader(reader);

                return employees;
            }
            else
            {
                return null;
            }

        }

        #endregion

        #region GetById

        public Employee GetById(int id)
        {
            var reader = _dataBase.GetReader(Procedure.GetEmployeeById, "@EmployeeId", id);

            if (reader != null && reader.Read())
            {
                var employee = ConvertToEmployee(reader);

                reader.NextResult(); // GET PHONES
                if (reader.HasRows)
                {
                    employee.Phones = ConvertToPhone(reader);
                }

                reader.NextResult(); // GET EMAILS
                if (reader.HasRows)
                {
                    employee.Emails = ConvertToEmail(reader);
                }

                _dataBase.CloseReader(reader);

                return employee;
            }
            else return null;
        }


        #endregion

        #region Create

        public int Create(Employee employee)
        {
            int EmployeeId;

            SqlParameter[] parameters =
            {
              new SqlParameter("@LastName ",employee.LastName),
              new SqlParameter("@FirstName",employee.FirstName),
              new SqlParameter("@MiddleName",employee.MiddleName),
              new SqlParameter("@DepartmentId",employee.DepartmentId),
              new SqlParameter("@Address",employee.Address),
              new SqlParameter("@ImagePath",employee.ImagePath),
              new SqlParameter("@BeginningOfWork",employee.BeginningOfWork),
              new SqlParameter("@EndOfWork",employee.EndOfWork),
              new SqlParameter("@MaritalStatusId",employee.MaritalStatus)
            };

            EmployeeId = Convert.ToInt32(_dataBase.GetScalar(Procedure.AddEmployee, parameters));

            return EmployeeId;
        }

        #endregion

        #region Upadte

        public int Update(Employee employee)
        {
            SqlParameter[] parameters =
             {
              new SqlParameter("@LastName ",employee.LastName),
              new SqlParameter("@FirstName",employee.FirstName),
              new SqlParameter("@MiddleName",employee.MiddleName),
              new SqlParameter("@DepartmentId",employee.DepartmentId),
              new SqlParameter("@Address",employee.Address),
              new SqlParameter("@ImagePath",employee.ImagePath),
              new SqlParameter("@BeginningOfWork",employee.BeginningOfWork),
              new SqlParameter("@EndOfWork",employee.EndOfWork),
              new SqlParameter("@MaritalStatusId",employee.MaritalStatus)
            };

            return _dataBase.ExecuteNonQuery(Procedure.UpdateEmployee, parameters);

        }

        #endregion

        #region Delete
        public int Delete(int id)
        {
            return _dataBase.ExecuteNonQuery(Procedure.DeleteEmployee, "@EmployeeId", id);
        }

        #endregion

        #region Converters
        private Employee ConvertToEmployee(SqlDataReader reader)
        {
            return new Employee()
            {
                Id = (int)reader["EmployeeId"],
                FirstName = reader["FirstName"] == DBNull.Value ? null : (string)reader["FirstName"],
                MiddleName = reader["MiddleName"] == DBNull.Value ? null : (string)reader["MiddleName"],
                LastName = reader["LastName"] == DBNull.Value ? null : (string)reader["LastName"],
                DepartmentId = reader["DepartmentId"] == DBNull.Value ? null : (int?)reader["DepartmentId"],
                MaritalStatus = reader["MaritalStatus"] == DBNull.Value ? null : (string)reader["MaritalStatus"],
                ImagePath = reader["ImagePath"] == DBNull.Value ? null : (string)reader["ImagePath"],
                BeginningOfWork = reader["BeginningOfWork"] == DBNull.Value ? null : (DateTime?)reader["BeginningOfWork"],
                EndOfWork = reader["EndOfWork"] == DBNull.Value ? null : (DateTime?)reader["EndOfWork"],
                Address = reader["Address"] == DBNull.Value ? null : (string)reader["Address"]
            };
        }


        private List<Phone> ConvertToPhone(SqlDataReader reader)
        {
            var phones = new List<Phone>();

            while (reader.Read())
            {
                phones.Add(new Phone()
                {
                    Kind = (KindOfPhone)reader["PhoneType"],
                    Number = (string)reader["Phone"]
                });
            }
            return phones;
        }



        private List<string> ConvertToEmail(SqlDataReader reader)
        {
            var emails = new List<string>();

            while (reader.Read())
            {
                emails.Add((string)reader["Email"]);
            }
            return emails;
        }

        #endregion

        #region Helpers

        private List<Phone> FindPhonesById(EnumerableRowCollection<DataRow> phones, int id)
        {
            var listOfPhones = from phone in phones
                               where phone.Field<int>("EmployeeId") == id
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


        private List<string> FindEmailsById(EnumerableRowCollection<DataRow> emails, int id)
        {
            var listOfemails = from email in emails
                               where email.Field<int>("EmployeeId") == id
                               select email.Field<string>("Email");

            if (listOfemails != null)
            {
                List<string> list = new List<string>();
                foreach (var item in listOfemails)
                {
                    list.Add(item);
                }

                return list;
            }
            else return null;
        }

        #endregion

    }

}

