using DM.PR.Common.Entities;
using DM.PR.Common.Enums;
using DM.PR.Data.Intefaces;
using System;
using System.Collections.Generic;
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
            return _departmentList.FirstOrDefault(d => d.Id == id);
        }

        public IEnumerable<Department> GetAll()
        {
            return _departmentList;
        }

        public void Create(Department item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public void Update(Department item)
        {
            throw new NotImplementedException();
        }
    }
}
