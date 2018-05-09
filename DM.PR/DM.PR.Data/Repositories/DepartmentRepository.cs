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
        private List<Department> _departmentList;

        public DepartmentRepository()
        {
            _departmentList = new List<Department>
            {

                new Department(){
                     Id =1,
                    Name = "Отдел кадров",
                        Address = " г.Минск, ул.Камомольская, д.30",
                        Description = " Отдел занимается работой с кадрами",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375297894512",
                                            Kind = KindOfPhone.HOME },
                            new Phone() { Number = " +375297894512",
                                            Kind = KindOfPhone.WORK }} },

                new Department(){ Id =2, ParentId =null,
                    Name = "Отдел сбыта",
                        Address = " г.Могилев, ул.Космонавтов, д.130",
                        Description = " Отдел занимается продажей продукции",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375291254789",
                                            Kind = KindOfPhone.HOME } } },

                 new Department(){ Id =3,
                     Name = "Отдел продаж",
                        Address = " г.Могилев, ул.Космонавтов, д.130",
                        Description = " Отдел занимается продажей продукции",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375291254789",
                                            Kind = KindOfPhone.HOME } } },
                 new Department(){ Id =4,
                     Name = "Бухгалтерия",
                        Address = " г.Могилев, ул.Космонавтов, д.130",
                        Description = " Отдел занимается продажей продукции",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375291254789",
                                            Kind = KindOfPhone.HOME} } },
                 new Department(){ Id =5,
                     Name = "Отдел связи",
                        Address = " г.Могилев, ул.Космонавтов, д.130",
                        Description = " Отдел занимается продажей продукции",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375291254789",
                                            Kind = KindOfPhone.HOME } } },
                 new Department(){ Id =6,
                     Name = "Управление",
                        Address = " г.Могилев, ул.Космонавтов, д.130",
                        Description = " Отдел занимается продажей продукции",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375291254789",
                                            Kind = KindOfPhone.HOME } } },
                  new Department(){  Id =7, ParentId = 9,
                      Name = "Питание",
                        Address = " г.Могилев, ул.Космонавтов, д.130",
                        Description = " Отдел занимается продажей продукции",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375291254789",
                                            Kind = KindOfPhone.HOME } } },
                   new Department(){ Id =8, ParentId =1,
                        Name = "Производство",
                        Address = " г.Могилев, ул.Космонавтов, д.130",
                        Description = " Отдел занимается продажей продукции",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375291254789",
                                            Kind = KindOfPhone.HOME } } },
                    new Department(){ Id =9, ParentId =1,
                        Name = "Производство",
                        Address = " г.Могилев, ул.Космонавтов, д.130",
                        Description = " Отдел занимается продажей продукции",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375291254789",
                                            Kind = KindOfPhone.HOME } } }

            };

        }


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
                    department.Address = (string)reader["Address"];
                    department.Description = (string)reader["Description"];
                }

                reader.NextResult();

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

        public IEnumerable<Department> GetAll()
        {
            return _departmentList;
        }

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

            DataBase.CloseConnection();
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public void Update(Department item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DepartmentNavModel> GetAllAsNavModel()
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
                        ParentId = reader["ParentID"] == DBNull.Value? null : (int?)reader["ParentID"]
                    });
                }
            }

            return AddChildren(list).Where(x => x.ParentId == null);
        }

        #region Helpers
        IEnumerable<DepartmentNavModel> AddChildren(IEnumerable<DepartmentNavModel> list)
        {
            foreach (var item in list)
            {
                var childrenList = list.Where(x => x.ParentId == item.Id);
                item.Cildren = childrenList != null ? AddChildren(childrenList) : null;
            }
            return list;
        }
        #endregion
    }
}
