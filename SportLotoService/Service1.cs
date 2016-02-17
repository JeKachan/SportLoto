using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SportLotoService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        TimerCode PartyStart = new TimerCode();
        SqlCode CheckFirstRun = new SqlCode();

        protected override void OnStart(string[] args)
        {
            CheckFirstRun.FirstRun();
            PartyStart.StartTimer();
        }

        protected override void OnStop()
        {
            PartyStart.StopTimer();
        }
    }
}
