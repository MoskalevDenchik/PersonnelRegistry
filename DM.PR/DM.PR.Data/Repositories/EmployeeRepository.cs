using DM.PR.Common.Entities;
using DM.PR.Common.Enums;
using DM.PR.Data.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DM.PR.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeesLis;

        public EmployeeRepository()
        {
            _employeesLis = new List<Employee>
            {
                new Employee(){ Id = 1, FirstName = "Федор", LastName = "Кемеров", MiddleName = "Васильевич",
                 Address = "г.Могилев, ул. Непокоренных, д.122, кв.30",
                 Phones = new List<Phone>{
                     new Phone(){ Number = "+375294563758", Kind = KindOfPhone.MOBILE},
                     new Phone(){ Number = "+375294563758", Kind = KindOfPhone.HOME},
                     new Phone(){ Number = "+375294563758", Kind = KindOfPhone.WORK}
                 },
                 Emails = new List<string>{
                     "derfe@mail.ru",
                     "ferser@mail.ru"
                 },
                 Department = new Department(){  Name = "Отдел кадров"},
                 MaritalStatus = MaritalStatus.MARRIED
                },
                new Employee(){Id = 2,  FirstName = "Иван", LastName = "Кемеров", MiddleName = "Васильевич",
                 Address = "г.Могилев, ул. Непокоренных, д.122, кв.30",
                  Phones = new List<Phone>{
                     new Phone(){ Number = "+375294563758", Kind = KindOfPhone.MOBILE},
                     new Phone(){ Number = "+375294563758", Kind = KindOfPhone.HOME},
                     new Phone(){ Number = "+375294563758", Kind = KindOfPhone.WORK}
                 },
                 Emails = new List<string>{
                     "derfe@mail.ru",
                     "ferser@mail.ru"
                 },
                 Department = new Department(){  Name = "Отдел продаж"},
                 MaritalStatus = MaritalStatus.MARRIED
                },
                new Employee(){Id = 3,  FirstName = "Николай", LastName = "Кемеров", MiddleName = "Васильевич",
                 Address = "г.Могилев, ул. Непокоренных, д.122, кв.30",
                  Phones = new List<Phone>{
                     new Phone(){ Number = "+375294563758", Kind = KindOfPhone.MOBILE},
                     new Phone(){ Number = "+375294563758", Kind = KindOfPhone.HOME},
                     new Phone(){ Number = "+375294563758", Kind = KindOfPhone.WORK}
                 },
                 Emails = new List<string>{
                     "derfe@mail.ru",
                     "ferser@mail.ru"
                 },
                 Department = new Department(){  Name = "Отдел сбыта"},
                 MaritalStatus = MaritalStatus.MARRIED
                }
            };
        }

        public void Create(Employee item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public Employee Get(int? id)
        {
            return _employeesLis.FirstOrDefault(d => d.Id == id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeesLis;
        }

        public void Update(Employee item)
        {
            throw new NotImplementedException();
        }
    }
}
