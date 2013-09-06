using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWorldClockTime.Business
{
    //“被观察对象”接口 
    public interface IObservable
    {
        void NotifyObservers(DateTime UTC);
    }
}
