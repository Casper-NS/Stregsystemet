using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet
{
    public class SeasonalProduct : Product
    {
        public SeasonalProduct(int id, string name, int price, bool active, bool creditStatus, DateTime startTime, DateTime endTime) 
            : base(id, name, price, active, creditStatus)
        {
            SeasonStartTime = startTime;
            SeasonEndTime = endTime;
        }

        public DateTime SeasonStartTime { get; }
        public DateTime SeasonEndTime { get; }

    }
}
