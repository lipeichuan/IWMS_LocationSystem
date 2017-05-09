using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class TimeOut
    {
        // 设定超时间隔为1000ms
        private readonly int TimeoutInterval = 1000;
        // lastTicks 用于存储新建操作开始时的时间
        public long lastTicks;
        // 用于存储操作消耗的时间
        public long elapsedTicks;
        public TimeOut(int timeoutInterval)
        {
            TimeoutInterval = timeoutInterval;
            lastTicks = DateTime.Now.Ticks;
        }
        public bool IsTimeout()
        {
            TimeSpan span = new TimeSpan(DateTime.Now.Ticks - lastTicks);
            double diff = span.TotalMilliseconds;
            //_nlog.Trace("timespan: {0}", diff);
            if (diff > TimeoutInterval)
                return true;
            else
                return false;
        }

        public void Restart()
        {
            lastTicks = DateTime.Now.Ticks;
        }
    }
}
