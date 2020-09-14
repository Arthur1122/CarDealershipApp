using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealership.Repository.EF;
using CarDealership.Web.API.ViewModels;
using CarDealershipApp.Domain;
using CarDealershipApp.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarDealership.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _carRepository;

        public CarsController(ICarRepository carRepository)
        {
            this._carRepository = carRepository;
        }


        // GET: api/<CarController>
        [HttpGet]
        public ActionResult<List<CarViewModel>> GetCars()
        {
            var cars = _carRepository.List().ToList();
            var viewCars = new List<CarViewModel>();
            foreach (var car in cars)
            {
                viewCars.Add(new CarViewModel
                {
                    Number = car.Number,
                    IsSold = car.IsSold,
                    Price = car.Price,
                    ClientName = car.Client != null ? car.Client.Name : null,
                    ClientPassportId = car.Client != null ? car.Client.PasportId : null
                });
            }

            return Ok(viewCars);
        }

        // GET api/<CarController>/number
        [HttpGet("{number}")]
        public ActionResult<CarViewModel> GetCar(string number)
        {
            var car = _carRepository.FindCar(number);
            
            if (car is null)
            {
                return StatusCode(404, "There is not such car");
            }
            CarViewModel viewCar = new CarViewModel
            {
                Number = car.Number,
                IsSold = car.IsSold,
                Price = car.Price,
                ClientName = car.Client != null ? car.Client.Name : null,
                ClientPassportId = car.Client != null ? car.Client.PasportId : null
            };
            return Ok(viewCar);
        }

        // POST api/<CarController>
        [HttpPost]
        public IActionResult Post([FromBody] CarViewModel car)
        {
            var createCar = Car.CreateCar(car.Number, car.Price);
            var result = _carRepository.Add(createCar);
            if (!result)
                return StatusCode(500);
            else
                return StatusCode(201);
        }
    }
}
