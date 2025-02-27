using Microsoft.EntityFrameworkCore;
using RedisCache.Models;

namespace RedisCache.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly EcommerceDbContext _context;

    public CustomerRepository(EcommerceDbContext context)
    {
        _context = context;
    }

    // 1. Lấy thông tin khách hàng theo ID từ SQL Server
    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        return await _context.Customers.FindAsync(customerId);
    }

    // 2. Lấy danh sách tất cả khách hàng
    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    // 3. Thêm hoặc cập nhật khách hàng vào SQL Server
    public async Task SetCustomerAsync(Customer customer)
    {
        var existingCustomer = await _context.Customers.FindAsync(customer.CustomerId);
        existingCustomer.Orders = null;
        existingCustomer.Reviews = null;
        if (existingCustomer == null)
        {
            _context.Customers.Add(customer);
        }
        else
        {
            _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
        }

        await _context.SaveChangesAsync();
    }

    // 4. Xóa khách hàng khỏi SQL Server
    public async Task RemoveCustomerAsync(int customerId)
    {
        var customer = await _context.Customers.FindAsync(customerId);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }

    // 5. Kiểm tra khách hàng có tồn tại trong SQL Server không
    public async Task<bool> ExistsAsync(int customerId)
    {
        return await _context.Customers.AnyAsync(c => c.CustomerId == customerId);
    }
}