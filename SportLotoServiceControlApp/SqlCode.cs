using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            SqlCommand getCommand = new SqlCommand("Insert into Drawings Values(@WinNo,@CreateDate)", OpenSqlConnection());
            getCommand.Parameters.AddWithValue("@WinNo", "0");
            getCommand.Parameters.AddWithValue("@CreateDate", DateTime.Today);
           // getCommand.Parameters.AddWithValue("@EndDate", DateTime.Today.AddDays(7));

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
            getCommand.Parameters.AddWithValue("@EndDate", DateTime.Today);

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

        //Last Update For Driwings session
        public int DrawingsSessionClose()
        {
            SqlCommand getCommand = new SqlCommand("UPDATE Drawings SET IsCompleted=@WinNo WHERE IsCompleted='0'", OpenSqlConnection());
            getCommand.Parameters.AddWithValue("@IsCompleted", 1);

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


        //Get Winners
        //NEED TO BE CHANGED!!
        public void GetWiners()
        {
            SqlCommand getCommand = new SqlCommand("SELECT * from Drawings as D JOIN Tickets as T on D.Id = T.DrawingId where D.IsCompleted = '0' and D.WinNo = T.TicketNo ", OpenSqlConnection());
            SqlDataReader rd = null;
            WinnersData winner = new WinnersData();
            try
            {
                if (OpenSqlConnection().State == System.Data.ConnectionState.Closed)
                    getCommand.Connection.Open();
                rd = getCommand.ExecuteReader();

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        winner.WinnerUserID = rd["ApplicationUserId"].ToString();
                        winner.WinnerDrawingID = Convert.ToInt32(rd["DrawingId"]);
                        winner.WinnerTicketID = Convert.ToInt32(rd["TicketNo"]);
                        winner.WinnerIsPayed = 0;

                        InsertWinners(winner);
                    }
                }

                DrawingsSessionClose();
            }
            catch (Exception)
            {
                throw;
            }

        }


        //Insert Winners
        public int InsertWinners(WinnersData _winner)
        {
            SqlCommand getCommand = new SqlCommand("Insert into WinnersData Values(@WinnerUserID,@WinnerDrawingID,@WinnerTicketID,@WinnerIsPayed)", OpenSqlConnection());
            getCommand.Parameters.AddWithValue("@WinnerUserID", _winner.WinnerUserID);
            getCommand.Parameters.AddWithValue("@WinnerDrawingID", _winner.WinnerDrawingID);
            getCommand.Parameters.AddWithValue("@WinnerTicketID", _winner.WinnerTicketID);
            getCommand.Parameters.AddWithValue("@WinnerIsPayed", _winner.WinnerIsPayed);

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


        //Winner Data
        public class WinnersData
        {
            public string WinnerUserID { get; set; }
            public int WinnerDrawingID { get; set; }
            public int WinnerTicketID { get; set; }
            public byte WinnerIsPayed { get; set; }
        }

    }
}
