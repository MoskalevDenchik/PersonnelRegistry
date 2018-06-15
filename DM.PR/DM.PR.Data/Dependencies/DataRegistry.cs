using DM.PR.Data.Core.InputParameters.Creaters.Implement;
using DM.PR.Data.Core.DataBase.Converters.Implement;
using DM.PR.Data.Core.ParameterCreaters.Implement;
using DM.PR.Data.SpecificationCreators.Implement;
using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Data.Core.DataBase.Data.Implement;
using DM.PR.Data.Core.Converters.Implement;
using DM.PR.Data.Core.Context.Implement;
using DM.PR.Data.Repositories.Implement;
using DM.PR.Data.Core.ParameterCreaters;
using DM.PR.Data.SpecificationCreators;
using StructureMap.Configuration.DSL;
using DM.PR.Data.Core.Data.Implement;
using DM.PR.Data.Core.DataBase.Data;
using DM.PR.Common.Entities.Account;
using DM.PR.Data.Core.Converters;
using DM.PR.Data.Specifications;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Data.Core.Data;

namespace DM.PR.Data.Dependencies
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {

            For<DbExecuter>().Use<SqlDbExecuter>();

            For<IDataContext<Role>>().Use<DbContext<Role>>();
            For<IDataContext<User>>().Use<DbContext<User>>();
            For<IDataContext<Employee>>().Use<DbContext<Employee>>();
            For<IDataContext<BillBoard>>().Use<WcfBillBoardContext>();
            For<IDataContext<KindPhone>>().Use<DbContext<KindPhone>>();
            For<IDataContext<Department>>().Use<DbContext<Department>>();
            For<IDataContext<WorkStatus>>().Use<DbContext<WorkStatus>>();
            For<IDataContext<MaritalStatus>>().Use<DbContext<MaritalStatus>>();

            For<IRepository<Role>>().Use<Repository<Role>>();
            For<IRepository<User>>().Use<Repository<User>>();
            For<IRepository<Employee>>().Use<Repository<Employee>>();
            For<IRepository<BillBoard>>().Use<Repository<BillBoard>>();
            For<IRepository<KindPhone>>().Use<Repository<KindPhone>>();
            For<IRepository<Department>>().Use<Repository<Department>>();
            For<IRepository<WorkStatus>>().Use<Repository<WorkStatus>>();
            For<IRepository<MaritalStatus>>().Use<Repository<MaritalStatus>>();

            For<IParameterCreater<Role>>().Use<RoleParameterCreater>();
            For<IParameterCreater<User>>().Use<UserParameterCreater>();
            For<IParameterCreater<Employee>>().Use<EmployeeParameterCreater>();
            For<IParameterCreater<BillBoard>>().Use<BillBoardParameterCreater>();
            For<IParameterCreater<KindPhone>>().Use<KindPhoneParameterCreater>();
            For<IParameterCreater<Department>>().Use<DepartmentParameterCreater>();
            For<IParameterCreater<WorkStatus>>().Use<WorkStatusParameterCreater>();
            For<IParameterCreater<MaritalStatus>>().Use<MaritalStatusParameterCreater>();

            For<IConverter<Role>>().Use<RoleConverter>();
            For<IConverter<User>>().Use<UserConverter>();
            For<IConverter<Employee>>().Use<EmployeeConverter>();
            For<IConverter<KindPhone>>().Use<KindPhoneConverter>();
            For<IConverter<Department>>().Use<DepartmentConverter>();
            For<IConverter<WorkStatus>>().Use<WorkStatusConverter>();
            For<IConverter<MaritalStatus>>().Use<MaritalStatusConverter>();

            For<IUserParameterCreator>().Use<UserParameterCreater>();
            For<IEmployeeParameterCreater>().Use<EmployeeParameterCreater>();
            For<IDepartmentParameterCreater>().Use<DepartmentParameterCreater>();

            For<IUserSpecificationCreator>().Use<UserSpecificationCreator>();
            For<IEmployeeSpecificationCreator>().Use<EmployeeSpecificationCreator>();
            For<IDepartmentSpecificationCreator>().Use<DepartmentSpecificationCreator>();
        }
    }
}