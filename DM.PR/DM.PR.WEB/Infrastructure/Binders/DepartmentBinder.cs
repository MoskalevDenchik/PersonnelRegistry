using DM.PR.Common.Entities;
using System.Web.Mvc;
using System.Linq;


namespace DM.PR.WEB.Infrastructure.Bindings
{
    public class DepartmentBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var _prov = new ProviderHelper(bindingContext.ValueProvider);

            return new Department
            {
                Id = _prov.GetValueOrDefault<int>("Id"),
                Name = _prov.GetValueOrDefault<string>("Name"),
                Address = _prov.GetValueOrDefault<string>("Address"),
                Description = _prov.GetValueOrDefault<string>("Description"),
                ParentId = _prov.GetValueOrDefault<int>("ParentId"),
                Phones = _prov.GetPrefixesWhoContaints("Phones").Select(prefix => new Phone
                {
                    Id = _prov.GetValueOrDefault<int>($"{prefix}.Id"),
                    Number = _prov.GetValueOrDefault<string>($"{prefix}.Number"),

                }).ToList()
            };
        }
    }
}