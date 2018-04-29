using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.PR.Business.Interfaces;
using DM.PR.Common.Entities;

namespace DM.PR.Business.Services
{
    public class DepartmentServices : IDepartmentServices
    {
        private List<Department> _departmentList;

        public DepartmentServices()
        {
            _departmentList = new List<Department>
            {

                new Department(){ Name = "Отдел кадров",
                        Address = " г.Минск, ул.Камомольская, д.30",
                        Description = " Отдел занимается работой с кадрами",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375297894512",
                                            Kind = "work" } } },

                new Department(){ Name = "Отдел сбыта",
                        Address = " г.Могилев, ул.Космонавтов, д.130",
                        Description = " Отдел занимается продажей продукции",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375291254789",
                                            Kind = "work" } } },

                 new Department(){ Name = "Отдел продаж",
                        Address = " г.Могилев, ул.Космонавтов, д.130",
                        Description = " Отдел занимается продажей продукции",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375291254789",
                                            Kind = "work" } } },
                 new Department(){ Name = "Бухгалтерия",
                        Address = " г.Могилев, ул.Космонавтов, д.130",
                        Description = " Отдел занимается продажей продукции",
                        Phones = new List<Phone>{
                            new Phone() { Number = " +375291254789",
                                            Kind = "work" } } }

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
