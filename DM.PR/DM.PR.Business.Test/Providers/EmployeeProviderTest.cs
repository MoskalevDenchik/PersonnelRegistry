using Microsoft.VisualStudio.TestTools.UnitTesting;
using DM.PR.Business.Providers.Implement;
using DM.PR.Data.SpecificationCreators;
using System.Collections.Generic;
using DM.PR.Data.Specifications;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Services;
using System.Collections;
using System.Reflection;
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

        private IReadOnlyCollection<Employee> _testList;


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
            _cahing.Setup(c => c.Get<PagedData<Employee>>(It.IsAny<string>())).Returns<PagedData<Employee>>(null);

            //act
            var list = _provider.GetPage(1, 1, out int totalCount);

            //assert 
            _repository.Verify(r => r.FindBy(It.IsAny<ISpecification>()), Times.Never);
            _cahing.Verify(c => c.Add(It.IsAny<string>(), It.IsAny<PagedData<Employee>>(), It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void GetPage_CahicngReturnedNull_DataWasNotCahnged()
        {
            //arrange 
            var expectedList = GetPropertyValueList(_testList);
            int expectedTotalCount = 2;
            _cahing.Setup(c => c.Get<PagedData<Employee>>(It.IsAny<string>())).Returns(new PagedData<Employee>() { Data = _testList, TotalCount = expectedTotalCount });

            //act
            var actualList = _provider.GetPage(1, 1, out int totalCount);
            int actualTotalCount = totalCount;

            //assert     
            CollectionAssert.AreEqual(expectedList, GetPropertyValueList(actualList));
            Assert.AreEqual(expectedTotalCount, actualTotalCount);
        }
        #endregion

        #region Helpers

        private List<object> GetPropertyValueList<T>(T obj)
        {
            List<object> list = new List<object>();

            if (typeof(IEnumerable<IEntity>).IsAssignableFrom(typeof(T)))
            {
                foreach (var item in obj as IEnumerable)
                {
                    AddPropetyValues(item, list);
                }
            }
            else
            {
                AddPropetyValues(obj, list);
            }

            return list;
        }

        private void AddPropetyValues<T>(T obj, List<object> list)
        {
            if (obj == null)
            {
                list.Add(null);
                return;
            }

            PropertyInfo[] propetrties = obj.GetType().GetProperties();

            foreach (PropertyInfo item in propetrties)
            {
                Type itemType = item.PropertyType;

                if (typeof(IEnumerable<IEntity>).IsAssignableFrom(itemType))
                {
                    var entity = obj.GetType().GetProperty(item.Name).GetValue(obj);
                    foreach (var podElement in entity as IEnumerable)
                    {
                        AddPropetyValues(podElement, list);
                    }
                }
                else if (itemType.GetInterface(typeof(IEntity).Name) != null)
                {
                    var entity = obj.GetType().GetProperty(item.Name).GetValue(obj);
                    AddPropetyValues(entity, list);
                }
                else
                {
                    list.Add(item.GetValue(obj));
                }
            }
        }



        #endregion
    }
}
