using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWorldClockTime.Business
{
    public class ClockObserver:IObserver
    {
        private  List<DateTime> cityTime = new List<DateTime>();

        public  List<DateTime> SomeValue()
        {
            return cityTime; 
        } 

        public void NewyorkTime(Object sender, CorrectionEventArgs e)
        {
            int jetLag = -5;
            //纽约的夏时制起止时间为每年的3月10日至11月3日。
            DateTime startDate = DateTime.Parse(e.dt.Year.ToString() + "-03-10");
            DateTime endDate = DateTime.Parse(e.dt.Year.ToString() + "-11-03");
            cityTime.Add(DST(e.dt, startDate, endDate).AddHours(jetLag));
        }

        public void MoscowTime(Object sender, CorrectionEventArgs e)
        {
            int jetLag = 4;
            cityTime.Add(e.dt.AddHours(jetLag));
        }

        public void PekingTime(Object sender, CorrectionEventArgs e)
        {
            int jetLag = 8;
            cityTime.Add(e.dt.AddHours(jetLag));
        }

        public void SydneyTime(Object sender, CorrectionEventArgs e)
        {
            int jetLag = 10;
            cityTime.Add(e.dt.AddHours(jetLag));
        }

        public void LondonTime(Object sender, CorrectionEventArgs e)
        {
            int jetLag = 0;
            //伦敦夏时制起止时间为每年的3月31日至10月27日
            DateTime startDate = DateTime.Parse(e.dt.Year.ToString() + "-03-31");
            DateTime endDate = DateTime.Parse(e.dt.Year.ToString() + "-10-27");
            cityTime.Add(DST(e.dt, startDate, endDate).AddHours(jetLag));
        }

        private DateTime DST(DateTime dt, DateTime startDate, DateTime endDate)
        {
            
            if (dt >= startDate && dt <= endDate)
            {
                dt = dt.AddHours(1);
            }
            return dt;
        }
    }
}
