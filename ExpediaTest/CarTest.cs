using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;
using System.Collections;
using System.Collections.Generic;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [TestMethod()]
        public void TestCarGetsLocationFromDatabase()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();
            ArrayList location = new ArrayList();
            for (var i = 0; i < 10; i++)
            {
                location.Add("location " + i);
            }

            Expect.Call(mockDatabase.getCarLocation(5)).Return("location 5");
            Expect.Call(mockDatabase.getCarLocation(1)).Return("location 1");

            mocks.ReplayAll();

            var target = new Car(10);
            target.Database = mockDatabase;

            String carLocation = target.getCarLocation(5);
            Assert.AreEqual(carLocation, location[5]);

            carLocation = target.getCarLocation(1);
            Assert.AreEqual(carLocation, location[1]);

            mocks.VerifyAll();
        }

        [TestMethod()]
        public void TestCarGetsMileageFromDatabase()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();
            Int32 Miles = 50;

            Expect.Call(mockDatabase.Miles).PropertyBehavior();

            mocks.ReplayAll();

            mockDatabase.Miles = Miles;
            var target = new Car(10);
            target.Database = mockDatabase;

            int mileage = target.Mileage;
            Assert.AreEqual(mileage, Miles);

            mocks.VerifyAll();
        }

        [TestMethod()]
        public void TestObjectMotherUnderstanding()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();
            Int32 Miles = 70;

            Expect.Call(mockDatabase.Miles).PropertyBehavior();

            mocks.ReplayAll();

            mockDatabase.Miles = Miles;
            var target = ObjectMother.BMW();
            target.Database = mockDatabase;

            int mileage = target.Mileage;
            Assert.AreEqual(mileage, Miles);

            mocks.VerifyAll();
        }
	}
}
