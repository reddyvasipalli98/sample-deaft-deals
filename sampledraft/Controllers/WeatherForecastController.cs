using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sampledraft.Models;
using sampledraft.DAL;

namespace sampledraft.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private Dal DAL = new Dal();
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet("GetDeals")]
        public List<DealResponse> GetDeals()
        {
            List<Deal> collection = DAL.GetAllTasks();
            List<DealResponse> resData = new List<DealResponse>();
           foreach(var item in collection)
            {
                DealResponse res = new DealResponse();
                res.Id = item.Id.ToString();
                res.Title = item.Title;
                res.Description = item.Description;
                res.ImageUrl = item.ImageUrl;
                resData.Add(res);
            }

            return resData;
        }

    }
}
