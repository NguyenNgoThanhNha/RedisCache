using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RedisCache.Cache;
using RedisCache.Repository;
using RedisCache.Models; // Giả sử có model Customer

namespace RedisCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IRedisCacheService _redisCacheService;

        public CustomerController(ICustomerRepository customerRepository,
            IRedisCacheService redisCacheService)
        {
            _customerRepository = customerRepository;
            _redisCacheService = redisCacheService;
        }

        // 1. Lấy khách hàng theo ID
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerById(string customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(int.Parse(customerId));
            if (customer == null)
                return NotFound("Customer not found");

            return Ok(customer);
        }

        // 2. Lấy danh sách khách hàng từ cache
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = _redisCacheService.Get<IEnumerable<Customer>>("Customers");
            if(customers != null)
            {
                return Ok(customers);
            }
            customers = await _customerRepository.GetAllCustomersAsync();
            _redisCacheService.Set("Customers", customers);
            return Ok(customers);
        }

        // 3. Thêm hoặc cập nhật khách hàng
        [HttpPost]
        public async Task<IActionResult> SetCustomer([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest("Invalid customer data");

            await _customerRepository.SetCustomerAsync(customer);
            return Ok("Customer added/updated successfully");
        }

        // 4. Xóa khách hàng khỏi cache
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> RemoveCustomer(string customerId)
        {
            await _customerRepository.RemoveCustomerAsync(int.Parse(customerId));
            return Ok("Customer removed from cache");
        }

        // 5. Kiểm tra khách hàng có tồn tại trong cache không
        [HttpGet("exists/{customerId}")]
        public async Task<IActionResult> CheckCustomerExists(string customerId)
        {
            bool exists = await _customerRepository.ExistsAsync(int.Parse(customerId));
            return Ok(new { exists });
        }
    }
}
