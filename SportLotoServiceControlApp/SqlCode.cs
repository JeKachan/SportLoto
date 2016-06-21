using SportLoto.DbModels;
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

        Setting _settings;
        protected Setting Settings
        {
            get
            {
                if (_settings == null)
                {
                    using (var db = new ApplicationDbContext())
                    {
                        _settings = db.Settings.FirstOrDefault();
                    }
                }
                return _settings;
            }
        }


        //SQL Connection
        //public SqlConnection OpenSqlConnection()
        //{
        //    //SqlConnection conn = new SqlConnection("Server=172.16.1.9;Database=JackpotLoto;User Id=sa;Password=info11824;");
        //    var conn = new SqlConnection(@"data source=.\HOMESQL;Initial Catalog=SportLoto;Integrated Security=True;");
        //    return conn;
        //}


        //insert Drawing
        public int SqlNewInsert()
        {
            var result = 0;

            using (var db = new ApplicationDbContext())
            {
                var today = DateTime.Today;
                var drawing = new Drawing();
                drawing.WinNo = "0";
                drawing.CreateDate = today;
                drawing.EndDate = today;
                drawing.IsCompleted = false;
                drawing.ToJackpotSum = 0;
                drawing.ToOwnerSum = 0;
                drawing.ToWinnersSum = 0;

                db.Drawings.Add(drawing);
                result = db.SaveChanges();

            }
            return result;

            //SqlCommand getCommand = new SqlCommand("Insert into Drawings Values(@WinNo, @EndDate, @IsCompleated, @CreateDate)", OpenSqlConnection());
            //getCommand.Parameters.AddWithValue("@WinNo", "0");
            //getCommand.Parameters.AddWithValue("@CreateDate", DateTime.Today);
            //getCommand.Parameters.AddWithValue("@EndDate", DateTime.Today);
            //getCommand.Parameters.AddWithValue("@IsCompleated", "0");

            //int ExecutedLines = 0;
            //try
            //{
            //    if (OpenSqlConnection().State == ConnectionState.Closed)
            //        getCommand.Connection.Open();
            //    ExecutedLines = getCommand.ExecuteNonQuery();
            //}
            //catch (Exception e)
            //{
            //    throw;
            //}
            //return ExecutedLines;
        }


        //Update Drawing
        public int SqlNewUpdate()
        {
            var executedLines = 0;
            using (var db = new ApplicationDbContext())
            {
                var drawing = db.Drawings.FirstOrDefault(x => x.WinNo == "0");
                var totalDrawingSum = drawing.Transactions.Count > 0 ? drawing.Transactions.Sum(x => x.ItemTotal) : 0;

                //var jackpotMainFondSum = db.MainFonds.Any() ? db.MainFonds.Sum(x => x.Drawing.ToJackpotSum) : 0;
                var jackpotMainFondSum = db.MainFonds.Any() ? db.MainFonds.Sum(x => x.IncrementSum) : 0;
                var enableJackPot = Settings.OwnerFond < jackpotMainFondSum + drawing.ToJackpotSum;

                var winNo = "";
                if (!enableJackPot)
                {
                    while (true)
                    {
                        winNo = GenRandom();
                        var jackpotWinCount = drawing.Tickets.Count(x => MatchCount(x, winNo) == Settings.DigitsNoInTicket);
                        if (jackpotWinCount == 0)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    winNo = GenRandom();
                }

                drawing.ToJackpotSum = totalDrawingSum * Settings.JackpotPart;
                drawing.ToOwnerSum = totalDrawingSum * Settings.OwnerPart;
                drawing.ToWinnersSum = totalDrawingSum * Settings.WinnersPart;
                drawing.WinNo = winNo;
                drawing.EndDate = DateTime.Today;

                var mainFond = new MainFond();
                var jackpotWinTikets = drawing.Tickets.Where(x => MatchCount(x, winNo) == Settings.DigitsNoInTicket);
                if (enableJackPot && jackpotWinTikets.Any())
                {
                    //todo need to check
                    mainFond.IncrementSum = -1 * jackpotMainFondSum;
                    drawing.ToOwnerSum += drawing.ToJackpotSum;
                    drawing.ToJackpotSum = 0;

                }
                else
                {
                    mainFond.IncrementSum = drawing.ToJackpotSum;
                }
                mainFond.DateCreate = DateTime.Today;
                mainFond.DrawingId = drawing.Id;
                db.MainFonds.Add(mainFond);

                executedLines = db.SaveChanges();
            }
            return executedLines;
        }

        //Last Update For Driwings session
        public int DrawingsSessionClose()
        {
            var executedLines = 0;
            using (var db = new ApplicationDbContext())
            {
                var drawing = db.Drawings.Where(x => x.IsCompleted == false).FirstOrDefault();
                drawing.EndDate = DateTime.Today;
                drawing.IsCompleted = true;
                executedLines = db.SaveChanges();
            }

            return executedLines;
            //SqlCommand getCommand = new SqlCommand("UPDATE Drawings SET IsCompleted=@IsCompleted, EndDate=@EndDate WHERE IsCompleted='0'", OpenSqlConnection());
            //getCommand.Parameters.AddWithValue("@IsCompleted", 1);
            //getCommand.Parameters.AddWithValue("@EndDate", DateTime.Today);

            //int ExecutedLines = 0;
            //try
            //{
            //    if (OpenSqlConnection().State == ConnectionState.Closed)
            //        getCommand.Connection.Open();
            //    ExecutedLines = getCommand.ExecuteNonQuery();
            //}
            //catch (Exception e)
            //{
            //    throw;
            //}
            //return ExecutedLines;
        }



        //Generate Random Number
        public string GenRandom()
        {
            var winNoLst = new List<int>();
            for (int i = 0; i < Settings.DigitsNoInTicket; i++)
            {
                System.Threading.Thread.Sleep(1000);
                int randomNumber = RandomNo.Next(0, 46);
                winNoLst.Add(randomNumber);
            }
            return new JavaScriptSerializer().Serialize(winNoLst);
        }


        public void FirstRun()
        {
            using (var db = new ApplicationDbContext())
            {
                if (!db.Drawings.Any())
                {
                    SqlNewInsert();
                }
            }
            //SqlCommand getCommand = new SqlCommand("SELECT * from Drawings", OpenSqlConnection());
            //SqlDataReader rd = null;
            //try
            //{
            //    if (OpenSqlConnection().State == System.Data.ConnectionState.Closed)
            //        getCommand.Connection.Open();
            //    rd = getCommand.ExecuteReader();

            //    if (!rd.HasRows)
            //    {
            //        SqlNewInsert();
            //    }

            //}
            //catch (Exception)
            //{
            //    throw;
            //}


        }


        //Get Winners
        public void SetWiners()
        {
            using (db = new ApplicationDbContext())
            {
                var drawing = db.Drawings.Where(x => x.IsCompleted == false).FirstOrDefault();

                var winners = (from x in drawing.Tickets
                               let matchCount = MatchCount(x, drawing.WinNo)
                               where matchCount == Settings.DigitsNoInTicket || matchCount == 5 || matchCount == 4
                               select new WinnersData
                               {
                                   ApplicationUserId = x.ApplicationUserId,
                                   TicketId = x.Id,
                                   DrawingId = drawing.Id,
                                   NumberMatchCount = (byte)matchCount
                               }).ToList();

                db.WinnersData.AddRange(winners);
                db.SaveChanges();
                DrawingsSessionClose();
            }
        }

        public int MatchCount(Ticket ticket, string _winNo)
        {
            var js = new JavaScriptSerializer();
            var winNo = js.Deserialize<List<int>>(_winNo);
            var ticketNo = js.Deserialize<List<List<int>>>(ticket.TicketNo);
            var count = 0;
            for (var i = 0; i < winNo.Count; i++)
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
