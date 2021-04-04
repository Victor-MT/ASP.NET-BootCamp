using DevCars.API.InputModels;
using DevCars.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerServices _customerServices;
        public CustomersController(CustomerServices customerServices)
        {
            _customerServices = customerServices;
        }
        [HttpGet("{id}/orders/{orderid}")]
        public IActionResult GetOrder(int id, int orderid)
        {
            var order = _customerServices.GetOrder(id, orderid);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddCustomerInputModel model)
        {
            _customerServices.RegisterCustomer(model);

            return NoContent();
        }

        [HttpPost("{id}/orders")]
        public IActionResult PostOrder(int id, [FromBody] AddOrderInputModel model)
        {
            var registeredOrder = _customerServices.RegisterOrder(id, model);


            return CreatedAtAction(
                nameof(GetOrder),
                new { id = registeredOrder.IdCustomer, orderid = registeredOrder.Id},
                model
                );
        }
    }
} 
