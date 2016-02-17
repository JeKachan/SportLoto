using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SportLotoService
{
    public class TimerCode : System.Timers.Timer
    {
        SqlCode SqlCommand = new SqlCode();
        TimerCode aTimer = new TimerCode();

        public void StartTimer()
        {
            
            aTimer.Elapsed += new ElapsedEventHandler(TimeTick);
            aTimer.Interval = 14400000;
            aTimer.Enabled = true;
            aTimer.Start();

        }

        private void TimeTick(object sender, ElapsedEventArgs e)
        {
            SqlCommand.DateChecker();
        }

        public void StopTimer()
        {

            aTimer.Stop();

        }
    }
}
