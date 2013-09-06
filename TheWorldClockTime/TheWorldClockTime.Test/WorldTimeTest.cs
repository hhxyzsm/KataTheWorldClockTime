using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheWorldClockTime.Business;

namespace TheWorldClockTime.Test
{
    [TestClass]
    public class WorldTimeTest
    {
        //2013年9月2日，北京时间（UTC+8）周一早8:00，
        //伦敦时间（UTC+0，夏时制加1小时）周一早1:00，
        //莫斯科时间（UTC+4）周一早4:00 ，
        //悉尼时间（UTC+10）周一早10:00，
        //纽约时间（UTC-5，夏时制加1小时）周日晚20:00。
        [TestMethod]
        public void UTCTestMethod()
        {
            DateTime  UTC  = DateTime.Parse("2013-09-02 0:00");
            List<DateTime> expectTime = new List<DateTime>() 
                { 
                    DateTime.Parse("2013-09-02 8:00"), 
                    DateTime.Parse("2013-09-02 1:00"), 
                    DateTime.Parse("2013-09-02 4:00"),
                    DateTime.Parse("2013-09-02 10:00"),
                    DateTime.Parse("2013-09-01 20:00")
                };

            EqualTime(UTC, expectTime);
        }

        //若把北京时间调整为周一早9:00，相应地其余4个城市的时间都自动增加1小时。
        [TestMethod]
        public void UTCAddOneHTestMethod()
        {
            DateTime UTC = DateTime.Parse("2013-09-02 1:00");

            List<DateTime> expectTime = new List<DateTime>() 
                { 
                    DateTime.Parse("2013-09-02 9:00"), 
                    DateTime.Parse("2013-09-02 2:00"), 
                    DateTime.Parse("2013-09-02 5:00"),
                    DateTime.Parse("2013-09-02 11:00"),
                    DateTime.Parse("2013-09-01 21:00")
                };

            EqualTime(UTC, expectTime);

        }

        //若到了2013年10月28日，伦敦夏时制结束，
        //而纽约夏时制尚未结束，把伦敦时间调整为周一早0:00，
        //其余城市的时间相应自动变为：北京周一早8:00，
        //莫斯科周一早4:00，悉尼周一早10:00，纽约周日晚20:00.

        [TestMethod]
        public void UTCAndDSTTestMethod()
        {
            DateTime UTC = DateTime.Parse("2013-10-28 0:00");
            
            List<DateTime> expectTime = new List<DateTime>() 
                { 
                    DateTime.Parse("2013-10-28 8:00"), 
                    DateTime.Parse("2013-10-28 0:00"), 
                    DateTime.Parse("2013-10-28 4:00"),
                    DateTime.Parse("2013-10-28 10:00"),
                    DateTime.Parse("2013-10-27 20:00")
                };

            EqualTime(UTC, expectTime);
        }

        private void EqualTime(DateTime UTC, List<DateTime> expectTime)
        {
            TimeCorrection timeCorrection = new TimeCorrection();//真正的被观察者
            ClockObserver clockObserver = new ClockObserver();//观察者
            timeCorrection.ChangeTimeEvent+=clockObserver.PekingTime;//注册事件
            timeCorrection.ChangeTimeEvent+=clockObserver.LondonTime;
            timeCorrection.ChangeTimeEvent+=clockObserver.MoscowTime;
            timeCorrection.ChangeTimeEvent+=clockObserver.SydneyTime;
            timeCorrection.ChangeTimeEvent+=clockObserver.NewyorkTime;
            timeCorrection.SomeValue=UTC;//被观察者改变状态
            List<DateTime> actualTime=clockObserver.SomeValue();//观察者反馈现象
            for (int i = 0; i < expectTime.Count; i++)
            {
                Assert.AreEqual(expectTime[i], actualTime[i]);
            }
        }
    }
    
}
