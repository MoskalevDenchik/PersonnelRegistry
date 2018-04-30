using DM.PR.Business.Interfaces;
using DM.PR.Common.Entities;
using DM.PR.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.PR.Business.Providers
{
    public class EmployeeProvider : IEmployeeProvider
    {
        private List<Employee> _employeesLis;
 
        public EmployeeProvider()
        {
            _employeesLis = new List<Employee>
            {
                new Employee(){ FirstName = "Федор", LastName = "Кемеров", MiddleName = "Васильевич",
                 Address = "г.Могилев, ул. Непокоренных, д.122, кв.30",
                 Phones = new Phone(){ Number = "+375294563758", Kind = KindOfPhone.MOBILE},
                 Department = new Department(){  Name = "Отдел кадров"},
                 MaritalStatus = MaritalStatus.MARRIED, Emails = "derfe@mail.ru"
                },
                new Employee(){ FirstName = "Иван", LastName = "Кемеров", MiddleName = "Васильевич",
                 Address = "г.Могилев, ул. Непокоренных, д.122, кв.30",
                 Phones = new Phone(){ Number = "+375294563758", Kind = KindOfPhone.MOBILE},
                 Department = new Department(){  Name = "Отдел продаж"},
                 MaritalStatus = MaritalStatus.MARRIED, Emails = "derfe@mail.ru"
                },
                new Employee(){ FirstName = "Николай", LastName = "Кемеров", MiddleName = "Васильевич",
                 Address = "г.Могилев, ул. Непокоренных, д.122, кв.30",
                 Phones = new Phone(){ Number = "+375294563758", Kind = KindOfPhone.MOBILE},
                 Department = new Department(){  Name = "Отдел сбыта"},
                 MaritalStatus = MaritalStatus.MARRIED, Emails = "derfe@mail.ru"
                }
            };
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeesLis;
        }


    }
}

