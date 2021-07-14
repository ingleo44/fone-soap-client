using fone_soap_app.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fone_soap_app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private ISoapDemo _soapDemo;
        private INumberConversion _numberConversion;
        private IDian _dian;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ISoapDemo soapDemo, INumberConversion numberConversion,IDian dian, ILogger<WeatherForecastController> logger)
        {
            _soapDemo = soapDemo;
            _logger = logger;
            _numberConversion = numberConversion;
            _dian = dian;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _dian.GetStatus("2546");
            return new ObjectResult(result);
        }
    }
}
