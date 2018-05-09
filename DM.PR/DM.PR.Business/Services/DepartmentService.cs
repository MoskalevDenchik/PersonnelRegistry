using DM.PR.Business.Interfaces;
using DM.PR.Common.Entities;
using DM.PR.Data.Intefaces;
using System.Collections.Generic;

namespace DM.PR.Business.Services
{
    public class DepartmentService : IDepartmentService
    {
        private IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public void Delete(int? id)
        {
            _departmentRepository.Delete(id);
        }

        public void Edit(Department department)
        {
            _departmentRepository.Update(department);
        }

        public void Create(Department department)
        {
            _departmentRepository.Create(new Department()
            {
                Name = "Проетер",
                Address = "ebosnpidf",
                Description = "sefnenf",
                Phones = new List<Phone>()
                {
                    new Phone(){ Number = "+23423234", Kind = Common.Enums.KindOfPhone.MOBILE},
                    new Phone(){ Number = "+23343233234", Kind = Common.Enums.KindOfPhone.WORK}

                }
            });
        }
    }
}

