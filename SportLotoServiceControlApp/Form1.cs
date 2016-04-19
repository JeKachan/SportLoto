using SportLotoService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportLotoServiceControlApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //some methods
        TimerCode PartyStart = new TimerCode();
        SqlCode CheckFirstRun = new SqlCode();

        //consts
        int MillisecondsInSecond = 1000;
        int SecondsInMinute = 60;
        int MinutesInHour = 60;
        int HoursInDay = 24;
        int DaysInWeek = 7;

        //Timer
        int RemainingTime =1;

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            label4.Text = "";
        }

        private int GetCorrectNum()
        {
            //we have minutes value by default
            int Number = Convert.ToInt32(textBox1.Text) * MillisecondsInSecond * SecondsInMinute;
            int ConvertedNumber = new Int32();

            //Control which Radial Button is Checked
            if (radioButton1.Checked)
            {
                ConvertedNumber = Number;
            }
            else if (radioButton2.Checked)
            {
                ConvertedNumber = Number * MinutesInHour;
            }
            else if (radioButton3.Checked)
            {
                ConvertedNumber = Number * MinutesInHour * HoursInDay;
            }
            else if (radioButton4.Checked)
            {
                ConvertedNumber = Number * MinutesInHour * HoursInDay * DaysInWeek;
            }

            //Getting results and Change Global Variable
            RemainingTime = ConvertedNumber / 1000;

            return RemainingTime;
        }

        private int FromSecToMin()
        {
            int Minutes = RemainingTime / SecondsInMinute;

            return Minutes%60;
        }

        private int FromSecToHours()
        {
            int Hours = RemainingTime / (SecondsInMinute * MinutesInHour);

            return Hours%24;
        }

        private int FromSecToDays()
        {
            int Days = RemainingTime / (SecondsInMinute * MinutesInHour * HoursInDay);

            return Days;
        }


        private void button1_Click(object sender, EventArgs e)
        {

            CheckFirstRun.FirstRun();

            PartyStart.StartTimer(GetCorrectNum());

            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PartyStart.StopTimer();

            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (RemainingTime>0)
            {
                RemainingTime -= 1;
                //label4.Text = RemainingTime.ToString();
                label4.Text = FromSecToDays().ToString() + " days, " + FromSecToHours().ToString() + " hours, " + FromSecToMin().ToString() + " minutes, " + (RemainingTime % 60).ToString() + " seconds left";
            }
            else
            {
                PartyStart.StopTimer();

                timer1.Stop();
            }
        }
    }
}
