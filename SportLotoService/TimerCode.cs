using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SportLotoService
{
    public class TimerCode : Timer
    {
        private static Timer aTimer;

        public void StartTimer()
        {
            aTimer = new Timer();
            aTimer.Elapsed += TimeTick;
            aTimer.Interval = 14400000;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            aTimer.Start();

        }

        private static void TimeTick(object sender, ElapsedEventArgs e)
        {
            SqlCode Sql_Command = new SqlCode();

            Sql_Command.DateChecker();

        }

        public void StopTimer()
        {

            aTimer.Stop();

        }
    }
}
