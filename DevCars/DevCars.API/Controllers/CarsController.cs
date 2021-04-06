using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.Services;
using Microsoft.AspNetCore.Http;
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

        /*
        [HttpGet]
        public IActionResult GetImage()
        {
            Byte[] b = System.IO.File.ReadAllBytes(@"Persistence\Creating_migrationFile_commandLine.png");

            return File(b, "image/jpeg");
        }*/
        
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

        /// <summary>
        /// Register a car
        /// </summary>
        /// <remarks>
        /// Example Request :
        /// {
        ///     "brand": "Chevrolet",
        ///     "model": "Onix",
        ///     "vinCode": "ABC123",
        ///     "year": 2021,
        ///     "color": "grey",
        ///     "productionDate": "2021-03-19"
        /// }
        /// </remarks>
        /// <param name="model"> Entry data</param>
        /// <returns> Created car </returns>
        /// <response code="201"> Success Operation </response>
        /// <response code="400"> Bad Request Operation </response>
        [HttpPost]
        // The api will only show this kinda response in the documantation (by default the documantion will use the 200 response)
        [ProducesResponseType(StatusCodes.Status201Created)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
