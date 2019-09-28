using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForecastIO;
using ForecastIO.Extensions;
namespace cmsTelevision.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private static readonly string KEY = "893a8e2d39076c8c087ed06a392e75d5";
     //   private static readonly float latitude = 33.478f;
       // private static readonly float longtitude = -7.4322f;

        [HttpGet]
        public async Task<ForecastIOResponse> GetWeather()

        {
            var request = new ForecastIORequest  6EY, 33.478f, -7.4322f, DateTime.Now, Unit.si);
            var response = await request.GetAsync();
            return response;
        }
    }
}