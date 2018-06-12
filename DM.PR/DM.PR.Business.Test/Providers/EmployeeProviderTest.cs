using Microsoft.VisualStudio.TestTools.UnitTesting;
using DM.PR.Business.Providers.Implement;
using DM.PR.Data.SpecificationCreators;
using DM.PR.WEB.DependencyResolution;
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
        private EmployeeProvider _provider;
        private Mock<IСachingService> _cahing;
        private Mock<IRepository<Employee>> _repository;
        private Mock<IEmployeeSpecificationCreator> _specificationCreator;
        private readonly IEnityReflector _reflector;

        private IReadOnlyCollection<Employee> _testList;

        public EmployeeProviderTest()
        {
            _reflector = IoC.Initialize().GetInstance<IEnityReflector>();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _cahing = new Mock<IСachingService>();
            _specificationCreator = new Mock<IEmployeeSpecificationCreator>();
            _repository = new Mock<IRepository<Employee>>();
            _provider = new EmployeeProvider(_repository.Object, _specificationCreator.Object, _cahing.Object);

            _testList = new List<Employee>{
            new Employee
            {
                Id = 1,
                Address = "SameAddress",
                FirstName = "firstName",
                MiddleName = "midleName",
                LastName = "lastName",
                MaritalStatus = new MaritalStatus { Id = 2, Status = "SameStatus" },
                Department = new Department
                {
                    Id = 2,
                    Name = "SameName",
                    Address = "SameAddress",
                    ParentId = 1,
                    Description = "SameDescription",
                    Phones = new List<Phone> {
                        new Phone {
                            Id = 1,
                            Kind = new KindPhone { Id = 1, Kind = "SameKind" },
                            Number = "123"
                        },
                         new Phone {
                            Id = 2,
                            Kind = new KindPhone { Id = 2, Kind = "SameKind" },
                            Number = "123"
                        }
                    }
                },
                WorkStatus = new WorkStatus { Id = 3, Status = "SameStatus " },
                Phones = new List<Phone> {
                    new Phone
                    {
                        Id = 3,
                        Kind = new KindPhone { Id = 2, Kind = "SameKind" },
                        Number = "123"
                    },
                    new Phone {
                        Id = 4,
                        Kind = new KindPhone { Id = 2, Kind = "SameKind" },
                        Number = "123"
                    } },
                Emails = new List<Email>
                {
                    new Email { Id = 1, Address = "SameAddress" },
                    new Email { Id = 2, Address = "SameAddress" }
                },
                BeginningWork = new DateTime(2018, 1, 1),
                EndWork = new DateTime(2018, 1, 1),
                ImagePath = "String ",
            },
            new Employee
                {
                Id = 1,
                Address = "SameAddress",
                FirstName = "firstName",
                MiddleName = "midleName",
                LastName = "lastName",
                MaritalStatus = new MaritalStatus { Id = 2, Status = "SameStatus" },
                Department = new Department
                {
                    Id = 2,
                    Name = "SameName",
                    Address = "SameAddress",
                    ParentId = 1,
                    Description = "SameDescription",
                    Phones = new List<Phone> {
                        new Phone {
                            Id = 1,
                            Kind = new KindPhone { Id = 1, Kind = "SameKind" },
                            Number = "123"
                        },
                         new Phone {
                            Id = 2,
                            Kind = new KindPhone { Id = 2, Kind = "SameKind" },
                            Number = "123"
                        }
                    }
                },
                WorkStatus = new WorkStatus { Id = 3, Status = "SameStatus " },
                Phones = new List<Phone> {
                    new Phone
                    {
                        Id = 3,
                        Kind = new KindPhone { Id = 2, Kind = "SameKind" },
                        Number = "123"
                    },
                    new Phone {
                        Id = 4,
                        Kind = new KindPhone { Id = 2, Kind = "SameKind" },
                        Number = "123"
                    } },
                Emails = new List<Email>
                {
                    new Email { Id = 1, Address = "SameAddress" },
                    new Email { Id = 2, Address = "SameAddress" }
                },
                BeginningWork = new DateTime(2018, 1, 1),
                EndWork = new DateTime(2018, 1, 1),
                ImagePath = "String ",
            }
            };
        }


        #region GetPageMethod

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetPage_PageSize0_ThrowException()
        {
            _provider.GetPage(0, 1, out int totalCount);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetPage_PageNumber0_ThrowException()
        {
            _provider.GetPage(1, 0, out int totalCount);
        }

        [TestMethod]
        public void GetPage_CahicngReturnedNotNull_FindByWasNotCalled()
        {
            //arrange 
            _cahing.Setup(c => c.Get<PagedData<Employee>>(It.IsAny<string>())).Returns(It.IsAny<PagedData<Employee>>());

            //act
            _provider.GetPage(1, 1, out int totalCount);

            //assert     
            _repository.Verify(r => r.FindBy(It.IsAny<ISpecification>()), Times.Never);
        }

        [TestMethod]
        public void GetPage_CahicngReturnedNull_FindByAndAddMethodsWasCalled()
        {
            //arrange
            int pageSize = 1;
            int PageNumber = 1;
            _cahing.Setup(c => c.Get<PagedData<Employee>>(It.IsAny<string>())).Returns<PagedData<Employee>>(null);

            //act
            var list = _provider.GetPage(pageSize, PageNumber, out int totalCount);

            //assert 
            _specificationCreator.Verify(c => c.CreateFindByPageDataSpecification(pageSize, PageNumber), Times.Once);
            _repository.Verify(r => r.FindBy(It.IsAny<ISpecification>(), out totalCount), Times.Once);
            _cahing.Verify(c => c.Add(It.IsAny<string>(), It.IsAny<PagedData<Employee>>(), It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void GetPage_CahicngReturnedNull_DataWasNotCahnged()
        {
            //arrange 
            var expectedPropertyValueList = _reflector.GetPropertyValueList(_testList);
            int expectedTotalCount = 2;
            _cahing.Setup(c => c.Get<PagedData<Employee>>(It.IsAny<string>())).Returns(new PagedData<Employee>() { Data = _testList, TotalCount = expectedTotalCount });

            //act
            var actualList = _provider.GetPage(1, 1, out int totalCount);
            int actualTotalCount = totalCount;

            //assert     
            CollectionAssert.AreEqual(expectedPropertyValueList, _reflector.GetPropertyValueList(actualList));
            Assert.AreEqual(expectedTotalCount, actualTotalCount);
        }

        [TestMethod]
        public void GetPage_CahicngReturnedNotNull_DataWasNotCahnged()
        {
            //arrange 
            int pageSize = 1;
            int pageNumber = 1;
            int expectedTotalCount = 2;
            var expectedPropertyValueList = _reflector.GetPropertyValueList(_testList);

            _cahing.Setup(c => c.Get<PagedData<Employee>>(It.IsAny<string>())).Returns<PagedData<Employee>>(null);
            _specificationCreator.Setup(s => s.CreateFindByPageDataSpecification(pageSize, pageNumber)).Returns(It.IsAny<ISpecification>);
            _repository.Setup(s => s.FindBy(It.IsAny<ISpecification>(), out expectedTotalCount)).Returns(_testList);

            //act
            var actualList = _provider.GetPage(pageSize, pageNumber, out int totalCount);
            int actualTotalCount = totalCount;

            //assert     
            CollectionAssert.AreEqual(expectedPropertyValueList, _reflector.GetPropertyValueList(actualList));
            Assert.AreEqual(expectedTotalCount, actualTotalCount);
        }

        #endregion
    }
}
