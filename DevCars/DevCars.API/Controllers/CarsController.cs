using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/cars")]
    public class CarsController : ControllerBase
    {
       private readonly CarServices _carServices;
        public CarsController(CarServices carServices)
        {
            _carServices = carServices;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_carServices.GetAllCars());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var car = _carServices.GetCarById(id);

            if (car == null)
                return NotFound();

            return Ok(car);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddCarInputModel model)
        {
            var registeredCar =_carServices.RegisterCar(model);

            return CreatedAtAction(
                nameof(GetById),
                new { id = registeredCar .Id},
                model
                );
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCarInputModel model)
        {
            var update = _carServices.UpdateCar(id, model);

            if (!update)
                return NotFound();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delete = _carServices.Delete(id);
            if (!delete)
                return NotFound();

            return NoContent();
        }
    }
}
