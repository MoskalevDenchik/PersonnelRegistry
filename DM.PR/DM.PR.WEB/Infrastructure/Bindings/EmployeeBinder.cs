using DM.PR.Common.Entities;
using System.Web.Mvc;
using System.Linq;
using System;


namespace DM.PR.WEB.Infrastructure.Bindings
{
    public class EmployeeBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var _prov = new ProviderHelper(bindingContext.ValueProvider);

            return new Employee
            {
                Id = _prov.GetValueOrDefault<int>("Id"),
                Address = _prov.GetValueOrDefault<string>("Address"),
                Department = new Department { Id = _prov.GetValueOrDefault<int>("DepartmentId") },
                FirstName = _prov.GetValueOrDefault<string>("FirstName"),
                MiddleName = _prov.GetValueOrDefault<string>("MiddleName"),
                LastName = _prov.GetValueOrDefault<string>("LastName"),
                HomePhone = _prov.GetValueOrDefault<string>("HomePhone"),
                WorkPhone = _prov.GetValueOrDefault<string>("WorkPhone"),
                MobilePhone = _prov.GetValueOrDefault<string>("MobilePhone"),
                BeginningWork = _prov.GetValueOrDefault<DateTime>("BeginningWork"),
                EndWork = _prov.GetValueOrDefault<DateTime?>("EndWork"),
                ImagePath = _prov.GetValueOrDefault<string>("ImagePath"),
                MaritalStatus = new MaritalStatus { Id = _prov.GetValueOrDefault<int>("MaritalStatusId") },
                WorkStatus = new WorkStatus { Id = _prov.GetValueOrDefault<int>("WorkStatusId") },
                Emails = _prov.GetPrefixesWhoContaints("Emails").Select(prefix => new Email
                {
                    Id = _prov.GetValueOrDefault<int>($"{prefix}.Id"),
                    Address = _prov.GetValueOrDefault<string>($"{prefix}.Address"),

                }).ToList()
            };
        }
    }
}


