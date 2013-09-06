using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWorldClockTime.Business
{
    public class CorrectionEventArgs: EventArgs 
    { 
        public readonly DateTime dt;
        public CorrectionEventArgs(DateTime dt) 
        {
            this.dt = dt; 
        }
    }

}
