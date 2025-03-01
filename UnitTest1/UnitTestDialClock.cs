using Lab1_2;
using Lab3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Reflection;

namespace UnitTestClass1
{
    [TestClass]
    public class DialClockTests
    {

        [TestMethod]
        public void DefaultConstructor_SetsTimeTo12_0()
        {
            var clock = new Lab1_2.DialClock();

            int hours = clock.Hours;
            int minutes = clock.Minutes;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(12, hours);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, minutes);
        }

        [TestMethod]
        public void ParameterizedConstructor_SetsTimeCorrectly()
        {
            var clock = new Lab1_2.DialClock(3, 15);

            int hours = clock.Hours;
            int minutes = clock.Minutes;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(3, hours);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(15, minutes);
        }

        [TestMethod]
        public void SetInvalidHours_ThrowsArgumentOutOfRangeException()
        {
            var clock = new Lab1_2.DialClock();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<ArgumentOutOfRangeException>(() => clock.Hours = -1);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<ArgumentOutOfRangeException>(() => clock.Hours = 24);
        }

        [TestMethod]
        public void SetInvalidMinutes_ThrowsArgumentOutOfRangeException()
        {
            var clock = new Lab1_2.DialClock();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<ArgumentOutOfRangeException>(() => clock.Minutes = -1);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<ArgumentOutOfRangeException>(() => clock.Minutes = 60);
        }

        [TestMethod]
        public void CalculateAngle_CalculatesCorrectlyForGivenTime()
        {
            var clock = new Lab1_2.DialClock(5, 30);

            double angle = clock.CalculateAngle();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(15, angle, delta: 0.0001);
        }

        [TestMethod]
        public void StaticCalculateAngle_CalculatesCorrectlyForGivenTime()
        {
            double angle = Lab1_2.DialClock.CalculateAngle(5, 30);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(15, angle, delta: 0.0001);
        }

        [TestMethod]
        public void ObjectCount_IncrementsCorrectly()
        {
            int initialCount = Lab1_2.DialClock.GetObjectCount();

            var clock1 = new Lab1_2.DialClock();
            var clock2 = new Lab1_2.DialClock(3, 15);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(initialCount + 2, Lab1_2.DialClock.GetObjectCount());
        }

        [TestMethod]
        public void IncrementOperator_CorrectlyUpdatesTime()
        {
            var clock = new Lab1_2.DialClock(5, 30);

            clock++;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(5, clock.Hours);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(31, clock.Minutes);
        }

        [TestMethod]
        public void DecrementOperator_CorrectlyUpdatesTime()
        {
            var clock = new Lab1_2.DialClock(5, 30);

            clock--;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(5, clock.Hours);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(29, clock.Minutes);
        }

        [TestMethod]
        public void AddMinutes_CorrectlyUpdatesTime()
        {
            var clock = new Lab1_2.DialClock(5, 30);

            clock += 45;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(6, clock.Hours);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(15, clock.Minutes);
        }

        [TestMethod]
        public void SubtractMinutes_CorrectlyUpdatesTime()
        {
            var clock = new Lab1_2.DialClock(5, 30);

            clock -= 45;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(clock.Hours, 4);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(clock.Minutes, 45);
        }

        [TestMethod]
        public void CalculateAngle_CalculatesCorrectlyForVariousTimes()
        {
            var clock = new Lab1_2.DialClock(3, 0);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(90, clock.CalculateAngle(), delta: 0.0001);

            clock = new Lab1_2.DialClock(6, 30);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(15, clock.CalculateAngle(), delta: 0.0001);

            clock = new Lab1_2.DialClock(9, 15);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(172.5, clock.CalculateAngle(), delta: 0.0001);
        }



        public class DialClock
        {

            [TestMethod]
            public void TestStaticCalculateAngle()
            {
                int hours = 9;
                int minutes = 15;

                var angle = DialClockArray.CalculateAngle(hours, minutes);

                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(52.5, angle, 0.0001);
            }

            [TestMethod]
            public void TestConstructorWithParameters()
            {
                int expectedHours = 5;
                int expectedMinutes = 30;

                var clock = new DialClockArray(expectedHours, expectedMinutes);

                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedHours, clock.Hours);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedMinutes, clock.Minutes);
            }

            private int hours;
            private int minutes;

            public DialClock(int hours = 12, int minutes = 0)
            {
                this.hours = hours;
                this.minutes = minutes;
            }

            public static explicit operator bool(DialClock clock)
            {
                return clock.hours != 0 || clock.minutes != 0;
            }
        }

        [TestMethod]
        public void BoolConversion_ChecksMultipleof25()
        {
            var clock = new DialClock();

            bool result = (bool)clock;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(result);
        }

    }
}
