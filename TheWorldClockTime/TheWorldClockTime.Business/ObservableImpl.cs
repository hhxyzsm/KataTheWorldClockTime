using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWorldClockTime.Business
{
    //所有被观察对象的基类 
    public class ObservableImpl:IObservable
    {
        protected DateTime UTC;
        //声明委托       
        public delegate void CorrectionEventHandler(Object sender, CorrectionEventArgs e);
        //声明事件
        public event CorrectionEventHandler ChangeTimeEvent; 

        public ObservableImpl()
        {
            
        }
        
        //将事件通知观察者 
        public void NotifyObservers(DateTime UTC)
        {
            //枚举容器中的观察者，将事件一一通知给他们 
            if (ChangeTimeEvent != null)
            {
                //e 获取观察者兴趣的变量
                CorrectionEventArgs e = new CorrectionEventArgs(UTC);
                Delegate[] delArry = ChangeTimeEvent.GetInvocationList();
                foreach (Delegate item in delArry)
                {
                    item.DynamicInvoke(this, e);
                }
            }
        }
    }
}
