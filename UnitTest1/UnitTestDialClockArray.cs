using Lab3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Reflection;

namespace Lab3Tests
{
    [TestClass]
    public class DialClockTests
    {

        [TestMethod]
        public void TestDecrementOperator()
        {
            var clock = new DialClockArray(3, 45);

            clock--;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(3, clock.Hours);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(44, clock.Minutes);
        }

        [TestMethod]
        public void TestSubtractionOperator()
        {
            var clock = new DialClockArray(3, 45); 

            clock = clock - 30;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(3, clock.Hours); 
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(15, clock.Minutes);
        }


        [TestMethod]
        public void TestObjectCount_IncrementsCorrectly()
        {
            int initialCount = DialClockArray.GetObjectCount();
            var clock1 = new DialClockArray();
            var clock2 = new DialClockArray();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(initialCount + 2, DialClockArray.GetObjectCount());
        }


        [TestMethod]
        public void TestFindMaxAngle()
        {
            var clock = new DialClock(5);
            var maxClock = clock.FindMaxAngle();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(maxClock);
        }

        [TestMethod]
        public void TestCalculateAngleMinMax()
        {
            var clockMin = new DialClockArray(0, 0);
            var clockMax = new DialClockArray(12, 0);

                       double angleMin = clockMin.CalculateAngle();
            double angleMax = clockMax.CalculateAngle();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, angleMin, 0.0001); 
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, angleMax, 0.0001); 
        }

        [TestMethod]
        public void TestStaticCalculateAngle()
        {
            double angle = DialClockArray.CalculateAngle(3, 15);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(7.5, angle, 0.0001);
        }


        [TestMethod]
        public void TestFindMaxAngleEmptyArray()
        {
            var emptyClock = new DialClock();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNull(emptyClock.FindMaxAngle());
        }


        [TestMethod]
        public void TestGetInfo()
        {
            var clock = new DialClockArray(3, 45);
            string info = clock.GetInfo();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(info.Contains("3:45"));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(info.Contains("Угол между часовой и минутной стрелками"));
        }


        [TestMethod]
        public void DefaultConstructor_SetsTimeTo12_0()
        {
            var clock = new DialClockArray();

            int hours = clock.Hours;
            int minutes = clock.Minutes;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(12, hours);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, minutes);
        }

        [TestMethod]
        public void ParameterizedConstructor_SetsTimeCorrectly()
        {
            var clock = new DialClockArray(3, 15);

            int hours = clock.Hours;
            int minutes = clock.Minutes;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(3, hours);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(15, minutes);
        }

        [TestMethod]
        public void SetInvalidHours_ThrowsArgumentOutOfRangeException()
        {
            var clock = new DialClockArray();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<ArgumentOutOfRangeException>(() => clock.Hours = 25);
        }

        [TestMethod]
        public void SetInvalidMinutes_ThrowsArgumentOutOfRangeException()
        {
            var clock = new DialClockArray();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<ArgumentOutOfRangeException>(() => clock.Minutes = 60);
        }

        [TestMethod]
        public void CalculateAngle_CalculatesCorrectlyForGivenTime()
        {
            var clock = new DialClockArray(5, 30);

            double angle = clock.CalculateAngle();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(15, angle, 0.0001);
        }


        [TestMethod]
        public void SetHoursInRange_DoesNotThrowException()
        {
            var clock = new DialClockArray();

            for (int i = 0; i <= 23; i++)
            {
                clock.Hours = i;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(i, clock.Hours);
            }
        }

        [TestMethod]
        public void SetMinutesInRange_DoesNotThrowException()
        {
            var clock = new DialClockArray();

            for (int i = 0; i <= 59; i++)
            {
                clock.Minutes = i;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(i, clock.Minutes);
            }
        }

        [TestMethod]
        public void SetMinutesOutOfRange_ThrowsException()
        {
            var clock = new DialClockArray();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<ArgumentOutOfRangeException>(() => clock.Minutes = -1);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<ArgumentOutOfRangeException>(() => clock.Minutes = 60);
        }

        [TestMethod]
        public void CalculateAngle_CorrectlyHandlesEdgeCases()
        {
            var clock = new DialClockArray(12, 0);

            double angle = clock.CalculateAngle();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, angle % 360, 0.0001);
        }

        [TestMethod]
        public void CalculateAngleForMidnight_CorrectlyHandlesEdgeCases()
        {
            var clock = new DialClockArray(0, 0);

            double angle = clock.CalculateAngle();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, angle, 0.0001);
        }
        [TestMethod]
        public void TestArrayWithLargeAngles()
        {
            DialClockArray largeAnglesArray = new DialClockArray(new double[] { 720.0, 1080.0, 1440.0 });

            bool result = largeAnglesArray.HasLargeAngles();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(result, "Массивы с большими углами должны возвращать значение true");
        }

        [TestMethod]
        public void TestArrayWithNegativeAngles()
        {
            DialClockArray negativeAnglesArray = new DialClockArray(new double[] { -2.5, -5.0, -7.5 });

            bool result = (bool)negativeAnglesArray;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(result, "Массивы с отрицательной кратностью 2.5 должны возвращать значение true.");
        }

        [TestMethod]
        public void TestArrayWithNonMultiplesOf2_5()
        {
            DialClockArray nonMultiplesOf2_5Array = new DialClockArray(new double[] { 720.0, 1080.0, 1442.0 });

            bool result = nonMultiplesOf2_5Array.HasNonMultiplesOf2_5();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(result, "Массивы с углами не кратными 2.5 должны возвращать значение false.");
        }

        [TestMethod]
        public void TestMultipleElementsArray()
        {
            DialClockArray multipleElementsArray = new DialClockArray(new double[] { 2.5, 5.0, 7.5, 10.0 });

            bool result = (bool)multipleElementsArray;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(result, "Массивы с углами кратными 2.5 должны возвращать значение true.");
        }

    }

    [TestClass]
    public class DialClockArrayTests
    {
        [TestMethod]
        public void TestIndexer()
        {
            var clocks = new DialClock(3);
            clocks[0] = new DialClockArray(6, 15);
            clocks[1] = new DialClockArray(10, 45);
            clocks[2] = new DialClockArray(12, 30);

            var clock = clocks[1];

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(10, clock.Hours); 
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(45, clock.Minutes); 
        }

        [TestMethod]
        public void TestIncrementOperator()
        {
            var clock = new DialClockArray(12, 59);
            clock++;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(13, clock.Hours);  
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, clock.Minutes); 
        }

        [TestMethod]
        public void TestPlusOperator()
        {
            var clock = new DialClockArray(12, 30);
            clock = clock + 40;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(13, clock.Hours);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(10, clock.Minutes);
        }

        [TestMethod]
        public void TestIntCastOperator()
        {
            var clock = new DialClockArray(2, 45);
            int minutesSinceMidnight = (int)clock;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(165, minutesSinceMidnight);
        }


        [TestMethod]
        public void DialClockArray_DefaultConstructor_CreatesEmptyArray()
        {
            Lab3.DialClock array = new Lab3.DialClock();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void DialClockArray_CopyConstructor_CreatesDeepCopy()
        {
            Lab3.DialClock originalArray = new Lab3.DialClock(3);
            DialClockArray clock1 = originalArray[0];

            Lab3.DialClock copiedArray = new Lab3.DialClock(originalArray);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(originalArray.Length, copiedArray.Length);
            for (int i = 0; i < originalArray.Length; i++)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(originalArray[i].Hours, copiedArray[i].Hours);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(originalArray[i].Minutes, copiedArray[i].Minutes);
            }

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotSame(originalArray[0], copiedArray[0]);
        }

        [TestMethod]
        public void DialClockArray_Indexer_ValidIndex_ReturnsElement()
        {
            Lab3.DialClock array = new Lab3.DialClock(3);

            DialClockArray clock = array[1];

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(clock);
        }

        [TestMethod]
        public void TestArrayCopyConstructor()
        {
            var array1 = new DialClock(3);
            array1[0] = new DialClockArray(1, 30);
            array1[1] = new DialClockArray(2, 45);
            array1[2] = new DialClockArray(3, 15);

            var array2 = new DialClock(array1);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(3, array2.Length);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, array2[0].Hours); 
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(30, array2[0].Minutes);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, array2[1].Hours); 
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(45, array2[1].Minutes);
        }


        [TestMethod]
        public void DialClockArray_FindMaxAngle_ReturnsClockWithMaxAngle()
        {
            Lab3.DialClock array = new Lab3.DialClock(3);

            DialClockArray maxClock = array.FindMaxAngle();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(maxClock);
        }

        [TestMethod]
        public void DialClockArray_FindMaxAngle_EmptyArray_ReturnsNull()
        {
            Lab3.DialClock array = new Lab3.DialClock();

            DialClockArray maxClock = array.FindMaxAngle();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNull(maxClock);
        }
    }
}
