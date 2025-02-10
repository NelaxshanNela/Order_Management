using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Management.DTOs.RequesDTOs;
using Order_Management.IServices;
using Order_Management.Models;
using Order_Management.Services;

namespace Order_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var exsitingCustomers = await _service.GetAllCustomersAsync();
            if (exsitingCustomers == null) return BadRequest("No data found...");

            return Ok(exsitingCustomers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _service.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound("Invalid customer id...");
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerRequestDTO customerRequestDTO)
        {
            await _service.AddCustomerAsync(customerRequestDTO);
            return Ok("Created succesfully...");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CustomerRequestDTO customerRequestDTO)
        {
            await _service.UpdateCustomerAsync(customerRequestDTO, id);
            return Ok("Updated Succesfully...");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteCustomerAsync(id);
            return Ok("Deleted Successfully...");
        }
    }
}
