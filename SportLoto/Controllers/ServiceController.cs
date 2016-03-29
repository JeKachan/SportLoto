using SportLoto.DbModels;
using System;
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
    }
}