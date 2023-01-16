using ExportToExcel.Models;

namespace ExportToExcel.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeViewModel>> GetEmployees();
        Task<Employee> GetEmployee(int EmployeeId);
        Task<bool> PostEmployee(Employee employee);
        Task<bool> PutEmployee(int EmployeeId,Employee employee);
        Task<bool> DeleteEmployee(int EmployeeId);
    }
}
