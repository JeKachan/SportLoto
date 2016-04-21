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

        public void StartTimer(int _interval)
        {
            aTimer = new Timer();
            aTimer.Elapsed += TimeTick;
            aTimer.Interval = _interval;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            aTimer.Start();

        }

        private static void TimeTick(object sender, ElapsedEventArgs e)
        {
            SqlCode Sql_Command = new SqlCode();

            //Current Session Close
            Sql_Command.SqlNewUpdate();

            //Get winners of current session
            Sql_Command.GetWiners();

            //Start New One
            Sql_Command.SqlNewInsert();

        }

        public void StopTimer()
        {

            aTimer.Stop();

        }
    }
}
