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

                new Department(){ Name = "Отдел кадров",
                        Address = " г.Минск, ул.Камомольская, д.30",
                        Description = " Отдел занимается работой с кадрами",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375297894512",
                                            Kind = KindOfPhone.HOME },
                            new Phone() { Number = " +375297894512",
                                            Kind = KindOfPhone.WORK }} },

                new Department(){ Name = "Отдел сбыта",
                        Address = " г.Могилев, ул.Космонавтов, д.130",
                        Description = " Отдел занимается продажей продукции",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375291254789",
                                            Kind = KindOfPhone.HOME } } },

                 new Department(){ Name = "Отдел продаж",
                        Address = " г.Могилев, ул.Космонавтов, д.130",
                        Description = " Отдел занимается продажей продукции",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375291254789",
                                            Kind = KindOfPhone.HOME } } },
                 new Department(){ Name = "Бухгалтерия",
                        Address = " г.Могилев, ул.Космонавтов, д.130",
                        Description = " Отдел занимается продажей продукции",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375291254789",
                                            Kind = KindOfPhone.HOME} } },
                 new Department(){ Name = "Отдел связи",
                        Address = " г.Могилев, ул.Космонавтов, д.130",
                        Description = " Отдел занимается продажей продукции",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375291254789",
                                            Kind = KindOfPhone.HOME } } },
                 new Department(){ Name = "Управление",
                        Address = " г.Могилев, ул.Космонавтов, д.130",
                        Description = " Отдел занимается продажей продукции",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375291254789",
                                            Kind = KindOfPhone.HOME } } },
                  new Department(){ Name = "Питание",
                        Address = " г.Могилев, ул.Космонавтов, д.130",
                        Description = " Отдел занимается продажей продукции",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375291254789",
                                            Kind = KindOfPhone.HOME } } },
                   new Department(){ Name = "Производство",
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
