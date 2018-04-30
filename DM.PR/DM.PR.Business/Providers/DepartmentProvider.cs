using DM.PR.Business.Interfaces;
using DM.PR.Common.Entities;
using DM.PR.Common.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DM.PR.Business.Providers
{
    public class DepartmentProvider : IDepartmentProvider
    {
        private List<Department> _departmentList;

        public DepartmentProvider()
        {
            _departmentList = new List<Department>
            {

                new Department(){ Name = "Отдел кадров",
                        Address = " г.Минск, ул.Камомольская, д.30",
                        Description = " Отдел занимается работой с кадрами",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375297894512",
                                            Kind = KindOfPhone.HOME } } },

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
                                            Kind = KindOfPhone.HOME } } }

            };


        }

        public IEnumerable<Department> GetAll()
        {
            return _departmentList;
        }

        public IEnumerable<string> GetListOfName()
        {
            return _departmentList.Select(d => d.Name).ToList();
        }
    }
}
