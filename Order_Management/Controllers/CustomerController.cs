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
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var exsitingCustomers = await _service.GetAllCustomersAsync();

                if (exsitingCustomers == null)
                {
                    return BadRequest("No data found");
                }
                return Ok(exsitingCustomers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var exsitingCustomer = await _service.GetCustomerByIdAsync(id);

                if (exsitingCustomer == null)
                {
                    return BadRequest("Invalid customer id");
                }
                return Ok(exsitingCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerRequestDTO customerRequestDTO)
        {
            try
            {
                await _service.AddCustomerAsync(customerRequestDTO);
                return Ok(new { success = true, message = "Customer added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error adding Customer", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerRequestDTO customerRequestDTO)
        {
            try
            {
                await _service.UpdateCustomerAsync(customerRequestDTO, id);
                return Ok(new { success = true, message = "Customer Updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error Updating Customer", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _service.DeleteCustomerAsync(id);
                return Ok(new { success = true, message = "Customer Deleted Successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error Deleting Customer", error = ex.Message });
            }
        }
    }
}
