using SF2022UserNNLib;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEqualsToTestData()
        {
            Calculations calculations = new Calculations();
            calculations.beginWorkingTime = new TimeSpan(8, 0, 0);
            calculations.endWorkingTime = new TimeSpan(18, 0, 0);
            calculations.startTimes = new TimeSpan[]
            { 
                new TimeSpan(10,0,0),
                new TimeSpan(11,0,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0),
                new TimeSpan(16,50,0),
            };
            calculations.durations = new int[] {60, 30, 10, 10, 40};
            calculations.consultationTime = 30;
            string[] exceptedRes =
            {
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
                "17:30-18:00"
            };
            string[] res = calculations.AvailablePeriods();
            Assert.AreEqual(exceptedRes.ToString(), res.ToString());
        }

        [TestMethod]
        public void TestEquals15to15Half()
        {
            Calculations calculations = new Calculations();
            calculations.beginWorkingTime = new TimeSpan(8, 0, 0);
            calculations.endWorkingTime = new TimeSpan(18, 0, 0);
            calculations.startTimes = new TimeSpan[]
            {
                new TimeSpan(8,0,0),
                new TimeSpan(15,30,0)
            };
            calculations.durations = new int[] { 420, 510 };
            calculations.consultationTime = 30;
            string[] exceptedRes =
            {
                "15:00-15:30"
            };
            string[] res = calculations.AvailablePeriods();
            Assert.AreEqual(exceptedRes.ToString(), res.ToString());
        }

    }
}