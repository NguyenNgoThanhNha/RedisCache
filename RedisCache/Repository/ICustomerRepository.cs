using RedisCache.Models;

namespace RedisCache.Repository
{
    public interface ICustomerRepository
    {
        // Lấy thông tin khách hàng theo ID từ SQL Server
        Task<Customer?> GetCustomerByIdAsync(int customerId);

        // Lấy danh sách tất cả khách hàng từ SQL Server
        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        // Thêm hoặc cập nhật thông tin khách hàng trong SQL Server
        Task SetCustomerAsync(Customer customer);

        // Xóa khách hàng khỏi SQL Server
        Task RemoveCustomerAsync(int customerId);

        // Kiểm tra khách hàng có tồn tại trong SQL Server hay không
        Task<bool> ExistsAsync(int customerId);
    }
}