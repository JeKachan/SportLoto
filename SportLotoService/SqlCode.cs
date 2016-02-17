using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SportLotoService
{
    public class SqlCode
    {
        Random RandomNo = new Random();


        //SQL Connection
        public SqlConnection OpenSqlConnection()
        {
            SqlConnection conn = new SqlConnection("data source=.\\HOMESQL;Initial Catalog=SportLoto;Integrated Security=True; User Id=sa;Password=12345;");
            return conn;
        }



        //insert Drawing
        public int SqlNewInsert()
        {
            SqlCommand getCommand = new SqlCommand("Insert into Drawing Values(@WinNo,@CreateDate,@EndDate)", OpenSqlConnection());
            getCommand.Parameters.AddWithValue("@WinNo", "0");
            getCommand.Parameters.AddWithValue("@CreateDate", DateTime.Today.Date);
            getCommand.Parameters.AddWithValue("@EndDate", DateTime.Today.AddDays(7));

            int ExecutedLines = 0;
            try
            {
                if (OpenSqlConnection().State == ConnectionState.Closed)
                    OpenSqlConnection().Open();
                ExecutedLines = getCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            OpenSqlConnection().Close();
            return ExecutedLines;
        }


        //Update Drawing
        public int SqlNewUpdate()
        {
            SqlCommand getCommand = new SqlCommand("UPDATE Drawings SET WinNo=@WinNo WHERE WinNo='0'", OpenSqlConnection());
            getCommand.Parameters.AddWithValue("@WinNo", GenRandom());

            int ExecutedLines = 0;
            try
            {
                if (OpenSqlConnection().State == ConnectionState.Closed)
                    OpenSqlConnection().Open();
                ExecutedLines = getCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            OpenSqlConnection().Close();
            return ExecutedLines;
        }



        //Generate Random Number
        public string GenRandom()
        {
            string WinNumber = "";

            for (int i = 0; i < 6; i++)
            {
                System.Threading.Thread.Sleep(5000);
                int randomNumber = RandomNo.Next(0, 46);
                WinNumber = randomNumber + ",";
            }
            WinNumber = WinNumber.Remove(WinNumber.Length - 1);
            return WinNumber;

        }


        //DateChecker - Update and insert data automatically when the day comes
        public void DateChecker()
        {
            SqlCommand getCommand = new SqlCommand("SELECT * from Drawings WHERE EndDate=@EndDate", OpenSqlConnection());
            getCommand.Parameters.AddWithValue("@EndDate", DateTime.Today.Date);
            SqlDataReader rd = null;
            try
            {
                if (OpenSqlConnection().State == System.Data.ConnectionState.Closed)
                    OpenSqlConnection().Open();
                rd = getCommand.ExecuteReader();

                if (rd.HasRows)
                {
                    SqlNewUpdate();
                    SqlNewInsert();
                }

            }
            catch (Exception)
            {
                throw;
            }
            OpenSqlConnection().Close();

        }

        public void FirstRun()
        {
            SqlCommand getCommand = new SqlCommand("SELECT * from Drawings", OpenSqlConnection());
            SqlDataReader rd = null;
            try
            {
                if (OpenSqlConnection().State == System.Data.ConnectionState.Closed)
                    OpenSqlConnection().Open();
                rd = getCommand.ExecuteReader();

                if (!rd.HasRows)
                {
                    SqlNewInsert();
                }

            }
            catch (Exception)
            {
                throw;
            }
            OpenSqlConnection().Close();

        }
    }
}
