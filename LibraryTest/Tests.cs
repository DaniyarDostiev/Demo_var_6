using SF2022User01Lib;

namespace LibraryTest
{
    public class Tests
    {
        [Test]
        public void emptyListCheck()
        {
            TimeSpan[] startTimes = new TimeSpan[0];
            int[] durations = new int[] { 30, 30, 30 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(11, 0, 0);
            int consultationTime = 30;

            Calculations calculations = new Calculations();
            var actualException = Assert.Throws<Exception>(() => calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));

            Assert.AreEqual("one of the lists is empty", actualException.Message);
        }

        [Test]
        public void differentLengthsCheck()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(8, 0, 0),
                new TimeSpan(9, 0, 0),
                new TimeSpan(10, 0, 0),
                new TimeSpan(11, 0, 0)
            };
            int[] durations = new int[] { 30, 30, 30 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(11, 0, 0);
            int consultationTime = 30;

            Calculations calculations = new Calculations();
            var actualException = Assert.Throws<Exception>(() => calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));

            Assert.AreEqual("different array lengths between start time and duration", actualException.Message);
        }

        [Test]
        public void incorrectBeginWorkingTime()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(8, 0, 0),
                new TimeSpan(9, 0, 0),
                new TimeSpan(10, 0, 0)
            };
            int[] durations = new int[] { 30, 30, 30 };
            TimeSpan beginWorkingTime = new TimeSpan(11, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(8, 0, 0);
            int consultationTime = 30;

            Calculations calculations = new Calculations();
            var actualException = Assert.Throws<Exception>(() => calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));

            Assert.AreEqual("the start time is greater than the end time", actualException.Message);
        }

        [Test]
        public void incorrectConsultationTime()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(8, 0, 0),
                new TimeSpan(9, 0, 0),
                new TimeSpan(10, 0, 0)
            };
            int[] durations = new int[] { 30, 30, 30 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(11, 0, 0);
            int consultationTime = -1;

            Calculations calculations = new Calculations();
            var actualException = Assert.Throws<Exception>(() => calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));

            Assert.AreEqual("consultation time is negative or equal to zero", actualException.Message);
        }

        [Test]
        public void firstCheckOfValidOutput()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10, 00, 00),
                new TimeSpan(11, 00, 00),
                new TimeSpan(15, 00, 00),
                new TimeSpan(15, 30, 00),
                new TimeSpan(16, 50, 00)
            };

            int[] durations = new int[]
            {
                60, 30, 10, 10, 40
            };

            TimeSpan beginWorkingTime = new TimeSpan(8, 00, 00);
            TimeSpan endWorkingTime = new TimeSpan(18, 00, 00);
            int consultationTime = 30;


            Calculations calculations = new Calculations();
            string[] expectedArray = {  
                "08:00-08:30",
                "08:30-09:00",
                "09:00-09:30",
                "09:30-10:00",
                "11:30-12:00",
                "12:00-12:30",
                "12:30-13:00",
                "13:00-13:30",
                "13:30-14:00",
                "14:00-14:30",
                "14:30-15:00",
                "15:40-16:10",
                "16:10-16:40",
                "17:30-18:00" };
            string[] actualArray = calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            Assert.AreEqual(expectedArray, actualArray);
        }

        [Test]
        public void secondCheckOfValidOutput()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(7, 0, 0)
            };
            int[] durations = new int[] { 60 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(15, 0, 0);
            int consultationTime = 60;

            Calculations calculations = new Calculations();
            string[] expectedArray = {
                "08:00-09:00",
                "09:00-10:00",
                "10:00-11:00",
                "11:00-12:00",
                "12:00-13:00",
                "13:00-14:00",
                "14:00-15:00"};

            string[] actualArray = calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            Assert.AreEqual(expectedArray, actualArray);
        }

        [Test]
        public void thirdCheckOfValidOutput()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(8, 0, 0),
                new TimeSpan(9, 0, 0),
                new TimeSpan(10, 0, 0)
            };
            int[] durations = new int[] { 30, 30, 30 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(11, 0, 0);
            int consultationTime = 30;

            Calculations calculations = new Calculations();
            string[] expectedArray = {
                "08:30-09:00",
                "09:30-10:00",
                "10:30-11:00",};
            string[] actualArray = calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            Assert.AreEqual(expectedArray, actualArray);
        }

        [Test]
        public void fourthCheckOfValidOutput()
        {
            // Arrange
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(9, 0, 0),
                new TimeSpan(10, 0, 0)
            };
            int[] durations = new int[] { 30, 30 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(11, 0, 0);
            int consultationTime = 30;

            Calculations calculations = new Calculations();
            string[] expectedArray = {
                "08:00-08:30",
                "08:30-09:00",
                "09:30-10:00",
                "10:30-11:00"};
            string[] actualArray = calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            Assert.AreEqual(expectedArray, actualArray);
        }

        [Test]
        public void fifthCheckOfValidOutput()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(8, 0, 0),
                new TimeSpan(9, 0, 0)
            };
            int[] durations = new int[] { 30, 30 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(11, 0, 0);
            int consultationTime = 30;

            Calculations calculations = new Calculations();
            string[] expectedArray = {
                "08:30-09:00",
                "09:30-10:00",
                "10:00-10:30",
                "10:30-11:00"};
            string[] actualArray = calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            Assert.AreEqual(expectedArray, actualArray);
        }

        [Test]
        public void sixthCheckOfValidOutput()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(9, 0, 0),
                new TimeSpan(11, 0, 0)
            };
            int[] durations = new int[] { 120, 30 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(12, 0, 0);
            int consultationTime = 45;

            Calculations calculations = new Calculations();
            string[] expectedArray = {
                "08:00-08:45"};
            string[] actualArray = calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            Assert.AreEqual(expectedArray, actualArray);
        }
    }
}