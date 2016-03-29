using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Script.Serialization;

namespace SportLotoService
{
    public class SqlCode
    {
        Random RandomNo = new Random();

        //SQL Connection
        public SqlConnection OpenSqlConnection()
        {
            SqlConnection conn = new SqlConnection("Server=.\\HOMESQL;Database=SportLoto;User Id=sa;Password=12345;");
            return conn;
        }



        //insert Drawing
        public int SqlNewInsert()
        {
            SqlCommand getCommand = new SqlCommand("Insert into Drawings Values(@WinNo,@CreateDate,@EndDate)", OpenSqlConnection());
            getCommand.Parameters.AddWithValue("@WinNo", "0");
            getCommand.Parameters.AddWithValue("@CreateDate", DateTime.Today);
            getCommand.Parameters.AddWithValue("@EndDate", DateTime.Today.AddDays(7));

            int ExecutedLines = 0;
            try
            {
                if (OpenSqlConnection().State == ConnectionState.Closed)
                    getCommand.Connection.Open();
                ExecutedLines = getCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
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
                    getCommand.Connection.Open();
                ExecutedLines = getCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return ExecutedLines;
        }



        //Generate Random Number
        public string GenRandom()
        {
            var winNoLst = new List<int>();
            for (int i = 0; i < 6; i++)
            {
                System.Threading.Thread.Sleep(5000);
                int randomNumber = RandomNo.Next(0, 46);
                winNoLst.Add(randomNumber);
            }

            return new JavaScriptSerializer().Serialize(winNoLst);
        }


        //DateChecker - Update and insert data automatically when the day comes
        public void DateChecker()
        {
            SqlCommand getCommand = new SqlCommand("SELECT * from Drawings WHERE EndDate=@EndDate and WinNo='0'", OpenSqlConnection());
            getCommand.Parameters.AddWithValue("@EndDate", DateTime.Today);
            SqlDataReader rd = null;
            try
            {
                if (OpenSqlConnection().State == System.Data.ConnectionState.Closed)
                    getCommand.Connection.Open();
                rd = getCommand.ExecuteReader();

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        SqlNewUpdate();
                        SqlNewInsert();
                    }

                }

            }
            catch (Exception)
            {
                throw;
            }


        }

        public void FirstRun()
        {
            SqlCommand getCommand = new SqlCommand("SELECT * from Drawings", OpenSqlConnection());
            SqlDataReader rd = null;
            try
            {
                if (OpenSqlConnection().State == System.Data.ConnectionState.Closed)
                    getCommand.Connection.Open();
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


        }
    }
}
