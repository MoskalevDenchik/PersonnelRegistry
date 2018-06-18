using Microsoft.VisualStudio.TestTools.UnitTesting;
using DM.PR.Business.Providers.Implement;
using DM.PR.Data.SpecificationCreators;
using DM.PR.Common.Helpers.Implement;
using System.Collections.Generic;
using DM.PR.Data.Specifications;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Services;
using DM.PR.Common.Helpers;
using System;
using Moq;

namespace DM.PR.Business.Test.Providers
{
    [TestClass]
    public class EmployeeProviderTest
    {
        #region Private

        private readonly IEnityReflector _reflector;
        private EmployeeProvider _provider;
        private Mock<IСacheStorage> _cahing;
        private Mock<IRepository<Employee>> _repository;
        private Mock<IEmployeeSpecificationCreator> _specificationCreator;
        private IReadOnlyCollection<Employee> _testList;
        private Mock<ISpecification> _specification;

        #endregion

        #region Ctors
        public EmployeeProviderTest()
        {
            _reflector = new EnityReflector();
        }

        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            _cahing = new Mock<IСacheStorage>();
            _specificationCreator = new Mock<IEmployeeSpecificationCreator>();
            _repository = new Mock<IRepository<Employee>>();
            _specification = new Mock<ISpecification>();
            _provider = new EmployeeProvider(_repository.Object, _specificationCreator.Object);
            _testList = new List<Employee>{
            new Employee
            {
                Id = 1,
                Address = "SameAddress",
                FirstName = "firstName",
                MiddleName = "midleName",
                LastName = "lastName",
                MaritalStatus = new MaritalStatus { Id = 2, Status = "SameStatus" },
                Emails = new List<Email>
                {
                    new Email { Id = 1, Address = "SameAddress" },
                    new Email { Id = 2, Address = "SameAddress" }
                },
                BeginningWork = new DateTime(2018, 1, 1),
                EndWork = new DateTime(2018, 1, 1),
                ImagePath = "String ",
                HasRole = true,
                HomePhone  ="1234",
                MobilePhone ="1234",
                WorkPhone  ="1234",
                WorkStatus = new WorkStatus{  Id =1, Status = "SameStatus"},
                Department = new Department
                {
                    Id =1 ,
                    Address    = "SameAddress",
                    Name  = "SameName",
                    Description = "SameDeacription",
                    ParentId = 0,
                    Phones = new List<Phone>()
                    {
                        new Phone{ Id =1, Number ="123456" },
                        new Phone{ Id =2, Number ="123456" }
                    }
                }
            },
            new Employee
            {

                Id = 1,
                Address = "SameAddress",
                FirstName = "firstName",
                MiddleName = "midleName",
                LastName = "lastName",
                MaritalStatus = new MaritalStatus { Id = 2, Status = "SameStatus" },
                Emails = new List<Email>
                {
                    new Email { Id = 1, Address = "SameAddress" },
                    new Email { Id = 2, Address = "SameAddress" }
                },
                BeginningWork = new DateTime(2018, 1, 1),
                EndWork = new DateTime(2018, 1, 1),
                ImagePath = "String ",
                HasRole = true,
                HomePhone  ="1234",
                MobilePhone ="1234",
                WorkPhone  ="1234",
                WorkStatus = new WorkStatus{  Id =1, Status = "SameStatus"},
                Department = new Department
                {
                    Id =1 ,
                    Address    = "SameAddress",
                    Name  = "SameName",
                    Description = "SameDeacription",
                    ParentId = 0,
                    Phones = new List<Phone>()
                    {
                        new Phone{ Id =1, Number ="123456" },
                        new Phone{ Id =2, Number ="123456" }
                    }
                }
            }
            };
        }

        #region GetEmployees_PageSize_PageNumber

        [TestMethod]
        public void GetEmployees_InValid_PageSize_PageNumber_ReturnNull()
        {
            //arrange 
            int totalCount;

            //act
            var actual1 = _provider.GetEmployees(-1, 1, out totalCount);
            var actual2 = _provider.GetEmployees(1, -1, out totalCount);
            var actual3 = _provider.GetEmployees(-1, -1, out totalCount);
            var actual4 = _provider.GetEmployees(0, 1, out totalCount);
            var actual5 = _provider.GetEmployees(1, 0, out totalCount);

            //assert 
            Assert.AreEqual(totalCount, 0);
            Assert.AreEqual(actual1, null);
            Assert.AreEqual(actual2, null);
            Assert.AreEqual(actual3, null);
            Assert.AreEqual(actual4, null);
            Assert.AreEqual(actual5, null);

            _specificationCreator.Verify(r => r.CreateSpecification(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            _repository.Verify(r => r.FindBy(It.IsAny<ISpecification>(), out totalCount), Times.Never);
        }

        [TestMethod]
        public void GetEmployees_Valid_PageSize_PageNumber_ReturnEmployeeList()
        {
            //arrange 
            int totalCount = 2;
            int PageSize = 1;
            int PageNumber = 5;
            var expectedPropertyValueList = _reflector.GetPropertyValueList(_testList);

            _specificationCreator.Setup(r => r.CreateSpecification(PageSize, PageNumber)).Returns(_specification.Object);
            _repository.Setup(x => x.FindBy(_specification.Object, out totalCount)).Returns(_testList);

            //act
            var actual = _provider.GetEmployees(PageSize, PageNumber, out totalCount);

            //assert
            Assert.AreEqual(totalCount, 2);
            CollectionAssert.AreEqual(expectedPropertyValueList, _reflector.GetPropertyValueList(actual));
            _specificationCreator.Verify(r => r.CreateSpecification(PageSize, PageNumber), Times.Once);
            _repository.Verify(r => r.FindBy(_specification.Object, out totalCount), Times.Once);
        }

        #endregion

        #region GetEmployees_PageSize_PageNumber_DepartmentId

        [TestMethod]
        public void GetEmployees_InValid_PageSize_PageNumber_DepartmentId_ReturnNull()
        {
            //arrange 
            int totalCount;

            //act
            var actual = _provider.GetEmployees(-1, 1, 1, out totalCount);
            var actual2 = _provider.GetEmployees(1, -1, 1, out totalCount);
            var actual3 = _provider.GetEmployees(1, 1, -1, out totalCount);
            var actual4 = _provider.GetEmployees(0, 1, 1, out totalCount);
            var actual5 = _provider.GetEmployees(1, 0, 1, out totalCount);
            var actual6 = _provider.GetEmployees(1, 1, 0, out totalCount);

            //assert 
            Assert.AreEqual(totalCount, 0);
            Assert.AreEqual(actual, null);
            Assert.AreEqual(actual2, null);
            Assert.AreEqual(actual3, null);
            Assert.AreEqual(actual4, null);
            Assert.AreEqual(actual5, null);
            Assert.AreEqual(actual6, null);

            _specificationCreator.Verify(r => r.CreateSpecification(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            _repository.Verify(r => r.FindBy(It.IsAny<ISpecification>(), out totalCount), Times.Never);
        }

        [TestMethod]
        public void GetEmployees_NotValid_PageSize_PageNumber_DepartmentId_ReturnListEmployees()
        {
            //arrange 
            int totalCount = 2;
            int pageSize = 5;
            int pageNumber = 1;
            int departmentId = 1;
            var expectedPropertyValueList = _reflector.GetPropertyValueList(_testList);

            _specificationCreator.Setup(x => x.CreateSpecification(departmentId, pageSize, pageNumber)).Returns(_specification.Object);
            _repository.Setup(x => x.FindBy(_specification.Object, out totalCount)).Returns(_testList);

            //act
            var actual = _provider.GetEmployees(departmentId, pageSize, pageNumber, out totalCount);

            //assert
            Assert.AreEqual(totalCount, 2);
            CollectionAssert.AreEqual(expectedPropertyValueList, _reflector.GetPropertyValueList(actual));
            _specificationCreator.Verify(r => r.CreateSpecification(1, 5, 1), Times.Once);
            _repository.Verify(r => r.FindBy(_specification.Object, out totalCount), Times.Once);
        }

        #endregion

        #region GetEmployees_SearchParams

        [TestMethod]
        public void GetEmployees_InValid_SearchParams_ReturnNull()
        {
            //arrange 
            int totalCount;

            //act
            var actual = _provider.GetEmployees(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), -1, 1, 1, 1, 1, out totalCount);
            var actual2 = _provider.GetEmployees(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), 1, -1, 1, 1, 1, out totalCount);
            var actual3 = _provider.GetEmployees(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), 1, 1, -1, 1, 1, out totalCount);
            var actual4 = _provider.GetEmployees(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), 1, 1, 1, -1, 1, out totalCount);
            var actual5 = _provider.GetEmployees(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), 1, 1, 1, 1, -1, out totalCount);
            var actual6 = _provider.GetEmployees(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), 1, 1, 1, 0, 1, out totalCount);
            var actual7 = _provider.GetEmployees(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), 1, 1, 1, 1, 0, out totalCount);

            //assert 
            Assert.AreEqual(totalCount, 0);
            Assert.AreEqual(actual, null);
            Assert.AreEqual(actual2, null);
            Assert.AreEqual(actual3, null);
            Assert.AreEqual(actual4, null);
            Assert.AreEqual(actual5, null);
            Assert.AreEqual(actual6, null);
            Assert.AreEqual(actual7, null);

            _specificationCreator.Verify(r => r.CreateSpecification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            _repository.Verify(r => r.FindBy(It.IsAny<ISpecification>(), out totalCount), Times.Never);
        }

        [TestMethod]
        public void GetEmployees_Valid_SearchParams_ReturnListEmployees()
        {
            //arrange 
            int totalCount = 2;
            int pageSize = 5;
            int pageNumber = 1;
            string lastName = "lastName";
            string firstName = "firstName";
            string middleName = "middleName";
            int fromYear = 1;
            int toYear = 100;
            int workStatusId = 1;
            var expectedPropertyValueList = _reflector.GetPropertyValueList(_testList);

            _specificationCreator.Setup(x => x.CreateSpecification(lastName, firstName, middleName, fromYear, toYear, workStatusId, pageSize, pageNumber)).Returns(_specification.Object);
            _repository.Setup(x => x.FindBy(_specification.Object, out totalCount)).Returns(_testList);

            //act
            var actual = _provider.GetEmployees(lastName, firstName, middleName, fromYear, toYear, workStatusId, pageSize, pageNumber, out totalCount);

            //assert
            Assert.AreEqual(totalCount, 2);
            CollectionAssert.AreEqual(expectedPropertyValueList, _reflector.GetPropertyValueList(actual));
            _specificationCreator.Verify(r => r.CreateSpecification(lastName, "firstName", "middleName", fromYear, toYear, workStatusId, pageSize, pageNumber), Times.Once);
            _repository.Verify(r => r.FindBy(_specification.Object, out totalCount), Times.Once);
        }

        #endregion
    }
}
