using EmployeeHierarchy;
using NUnit.Framework;
using System;

namespace EmployeeHierarchyTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ExceptionIsThrownWhenOneEmployeesReportsToMoreThanOneManger()
        {
            Assert.Throws<Exception>(() => new Employees("Employee4,Employee2,500" +
                "\n" +
				"Employee3,Employee1,800," + 
				"\n" +
				"Employee1,,1000" +
                "\n" +
				"Employee5,Employee1,500" + 
                "\n " +
				"Employee2,Employee1,500" + // Error
                "\n" +
				"Employee2,Employee5,500"));
        }

		[Test]
		public void TestExceptionIsThrownWhenWeHaveMoreThanOneCEO()
		{
			Assert.Throws<Exception>(() => new Employees("Employee4,Employee2,500" +
				"\n" +
				"Employee3,Employee1,800," +
				"\n" +
				"Employee1,,1000" + 
				"\n" +
				"Employee4,,1000" + // error
				"\n" +
				"Employee5,Employee1,500" +
				"\n " +
				"Employee2,Employee5,500"));

		}

		[Test]
		public void TestExceptionisThrownWhenWehaveCircularReference()
		{
			Assert.Throws<Exception>(() => new Employees("Employee4,Employee2,500" +
				"\n" +
				"Employee3,Employee1,800," + 
				"\n" +
				"Employee1,Employee3,800," + // error
				"\n" +
				"Employee1,,1000" + 
				"\n" +
				"Employee5,Employee1,500" +
				"\n " +
				"Employee2,Employee5,500"));

		}

		[Test]
		public void TestExceptionisThrownWhenAllManagersAreNotListedInEmployessCell()
		{
			Assert.Throws<Exception>(() => new Employees("Employee4,Employee2,500" +
				"\n" +
				"Employee3,Employee1,800," +
				"\n" +
				"Employee1,,1000" + 
				"\n" +
				"Employee5,Employee7,500" + // error
				"\n " +
				"Employee2,Employee5,500"));

		}

		[Test]
		public void TestManagerBurgetsReturnsCorrect()
		{

			Employees emp = new Employees("Employee4,Employee2,500" +
				"\n" +
				"Employee3,Employee1,800," +
				"\n" +
				"Employee1,,1000" +
				"\n" +
				"Employee5,Employee1,500" +
				"\n " +
				"Employee2,Employee5,500");

			Assert.AreEqual(3800, emp.ManagerSalaryBudget("CEO"));

		}
	}
}