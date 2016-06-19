using Postal;
using SportLoto.DbModels;
using SportLoto.Models;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SportLoto.Controllers
{
    public class ServiceController : ApplicationController
    {
        // GET: Service
        public async Task<ContentResult> CreateDrawing()
        {
            var result = "Drawing was not created!";
            var random = new Random();
            var newDrawing = new Drawing
            {
                WinNo = new JavaScriptSerializer().Serialize(new int[]
                {
                    random.Next(1, 46),
                    random.Next(1, 46),
                    random.Next(1, 46),
                    random.Next(1, 46),
                    random.Next(1, 46),
                    random.Next(1, 46)
                })
            };
            if (await repository.CreateDrawingAsync(newDrawing))
            {
                result = $"Drawing: id:{newDrawing.Id}, CreateDate: {newDrawing.CreateDate}, Win number:{newDrawing.WinNo}";
            };
            return Content(result);
        }

        public ActionResult TestPostal()
        {
            var email = new RegistrationEmail("RegistrationEmail")
            {
                To = "jekachan7@gmail.com",
                UserName = "fri.hsh@gmail.com",
                From = "fri.hsh@gmail.com"
            };

            email.Send();
            return new EmailViewResult(email);
        }   
    }
}