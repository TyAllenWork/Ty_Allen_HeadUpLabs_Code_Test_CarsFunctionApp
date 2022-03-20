using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace CarsFunctionApp
{
    public static class CarsAPI
    {
        public static readonly List<Car> Cars = new List<Car>()

        {
            new Car {name="Ferarri", maxSpeed = "205mph" },
            new Car {name="Jaguar", maxSpeed = "190mph" },
            new Car {name="Porshe", maxSpeed = "192mph" }
        };

        [FunctionName("CreateCar")]
        public static async Task<IActionResult> CreateCar(
            [HttpTrigger(AuthorizationLevel.Anonymous,
                "post", Route = "cars")]
            HttpRequest req, TraceWriter log)
        {
            log.Info("Creating a new Car list item");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<CarCreateModel>(requestBody);

            var car = new Car() { name = input.name, maxSpeed = input.maxSpeed };
            Cars.Add(car);
            return new OkObjectResult(car);

        }

        [FunctionName("GetAllCars")]
        public static IActionResult GetAllCars(
            [HttpTrigger(AuthorizationLevel.Anonymous,
                "get", Route = "cars")]
            HttpRequest req, TraceWriter log)
        {
            log.Info("Getting Car list items");
            return new OkObjectResult(Cars);
        }

        [FunctionName("UpdateTask")]
        public static async Task<IActionResult> UpdateCar(
        [HttpTrigger(AuthorizationLevel.Anonymous,
                "put", Route = "cars/{id}")]
        HttpRequest req,
        TraceWriter log, string id)
        {
            var car = Cars.FirstOrDefault(t => t.Id == id);
            if (car == null)
            {
                return new NotFoundResult();
            }
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updated = JsonConvert.DeserializeObject<CarUpdateModel>(requestBody);

            return new OkObjectResult(car);
        }


        [FunctionName("DeleteCar")]
        public static IActionResult DeleteCar(
            [HttpTrigger(AuthorizationLevel.Anonymous,
                "delete", Route = "cars/{id}")]
            HttpRequest req,
            TraceWriter log, string id)
        {
            var car = Cars.FirstOrDefault(t => t.Id == id);
            if (car == null)
            {
                return new NotFoundResult();
            }
            Cars.Remove(car);
            return new OkResult();
        }
    }
}
