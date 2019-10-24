using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Datastore.V1;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DemoUserSaveAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var host = _configuration.GetValue<string>("EMULATOR_HOST");
            var project = _configuration.GetValue<string>("PROJECT_ID");

            var channel = new Channel(host, 8081, ChannelCredentials.Insecure);
            var client = DatastoreClient.Create(channel); //FirestoreClient.Create(channel); 
            //var db = FirestoreDb.Create("test-project", client);

            var db = DatastoreDb.Create(project, "", client);

            var rng = new Random();
            var forecast = new WeatherForecast
            {
                Date = DateTime.UtcNow,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            };

            var entity = new Entity()
            {
                Key = db.CreateKeyFactory("Forecast").CreateIncompleteKey(),
                ["Date"] = forecast.Date,
                ["TemperatureC"] = forecast.TemperatureC,
                ["Summary"] = forecast.Summary
            };
            entity.Key = db.CreateKeyFactory("Forecast").CreateIncompleteKey();
            var keys = db.Insert(new[] { entity });


            var results = await db.RunQueryAsync(new Query("Forecast"));

            var forecasts = new List<WeatherForecast>();
            foreach (var result in results.Entities)
            {
                forecasts.Add(new WeatherForecast
                {
                    Date = result.Properties["Date"].TimestampValue.ToDateTime(),
                    TemperatureC = (int)result.Properties["TemperatureC"].IntegerValue,
                    Summary = result.Properties["Summary"].StringValue
                });
            }


            return forecasts;
        }
    }
}
