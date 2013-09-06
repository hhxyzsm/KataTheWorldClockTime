using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWorldClockTime.Business
{
    //时间校正（真正的被观察对象）
    public class TimeCorrection : ObservableImpl
    {
        private DateTime _value;
        public TimeCorrection() : base() { }
        public DateTime SomeValue
        {
            set
            {
                _value = value;
                base.NotifyObservers(_value);//将改变的消息通知观察者 
            }
        }
    }
}
