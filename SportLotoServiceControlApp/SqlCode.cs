using SportLoto.DbModels;
using SportLoto.DbModels.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Serialization;

namespace SportLotoService
{
    public class SqlCode
    {
        ApplicationDbContext db;
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
            SqlCommand getCommand = new SqlCommand("Insert into Drawings Values(@WinNo, @EndDate, @IsCompleated, @CreateDate)", OpenSqlConnection());
            getCommand.Parameters.AddWithValue("@WinNo", "0");
            getCommand.Parameters.AddWithValue("@CreateDate", DateTime.Today);
            getCommand.Parameters.AddWithValue("@EndDate", DateTime.Today);
            getCommand.Parameters.AddWithValue("@IsCompleated", "0");

            int ExecutedLines = 0;
            try
            {
                if (OpenSqlConnection().State == ConnectionState.Closed)
                    getCommand.Connection.Open();
                ExecutedLines = getCommand.ExecuteNonQuery();
            }
            catch (Exception e)
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
            SqlCommand getCommand = new SqlCommand("UPDATE Drawings SET IsCompleted=@IsCompleted, EndDate=@EndDate WHERE IsCompleted='0'", OpenSqlConnection());
            getCommand.Parameters.AddWithValue("@IsCompleted", 1);
            getCommand.Parameters.AddWithValue("@EndDate", DateTime.Today);

            int ExecutedLines = 0;
            try
            {
                if (OpenSqlConnection().State == ConnectionState.Closed)
                    getCommand.Connection.Open();
                ExecutedLines = getCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw;
            }
            return ExecutedLines;
        }



        //Generate Random Number
        public string GenRandom()
        {
            var winNoLst = new List<int>();
            for (int i = 0; i < SportLotoSettings.DigitsNoInTicket; i++)
            {
                System.Threading.Thread.Sleep(1000);
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
        public void SetWiners()
        {
            using (db = new ApplicationDbContext())
            {
                var drawing = db.Drawings.Where(x => x.IsCompleted == false).FirstOrDefault();
                var winners = drawing.Tickets
                    .Where(x => MatchCount(x, drawing.WinNo) == SportLotoSettings.JackpotNoCount)
                    .Select(x => new WinnersData
                    {
                        ApplicationUserId = x.ApplicationUserId,
                        TicketId = x.Id,
                        DrawingId = drawing.Id
                    }).ToList();

                db.WinnersData.AddRange(winners);
                db.SaveChanges();
                DrawingsSessionClose();
            }

            //SqlCommand getCommand = new SqlCommand("SELECT T.ApplicationUserId, T.Id as TicketId, D.Id as DrawingId from Drawings as D JOIN Tickets as T on D.Id = T.DrawingId where D.IsCompleted = '0' and D.WinNo = T.TicketNo ", OpenSqlConnection());
            //SqlDataReader rd = null;
            //WinnersData winner = new WinnersData();
            //try
            //{
            //    if (OpenSqlConnection().State == System.Data.ConnectionState.Closed)
            //        getCommand.Connection.Open();
            //    rd = getCommand.ExecuteReader();

            //    if (rd.HasRows)
            //    {
            //        while (rd.Read())
            //        {
            //            winner.ApplicationUserId = rd["ApplicationUserId"].ToString();
            //            winner.DrawingId = Convert.ToInt32(rd["DrawingId"]);
            //            winner.TicketId = Convert.ToInt32(rd["TicketId"]);
            //            winner.PaymentMade = false;

            //            InsertWinners(winner);
            //        }
            //    }

            //    DrawingsSessionClose();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        public int MatchCount(Ticket ticket, string _winNo)
        {
            var js = new JavaScriptSerializer();
            var winNo = js.Deserialize<List<int>>(_winNo);
            var ticketNo = js.Deserialize<List<List<int>>>(ticket.TicketNo);
            var count = 0;
            for(var i = 0; i < winNo.Count; i++)
            {
                if (ticketNo[i].Contains(winNo[i]))
                {
                    count++;
                }
            }
            return count;
        }
    }
}
