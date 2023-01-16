using AutoMapper;
using ExportToExcel.Models;
using Microsoft.EntityFrameworkCore;

namespace ExportToExcel.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly UnitOfWork _unitOfWork;
        private IMapper mapper;
        public EmployeeService(UnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<bool> DeleteEmployee(int EmployeeId)
        {
            var res =await _unitOfWork.Database.ExecuteSqlRawAsync($"Delete from Employees where EmployeeId={EmployeeId}");
            return res == 1;
        }

        public async Task<Employee> GetEmployee(int EmployeeId) =>
           await _unitOfWork.Employees.SingleOrDefaultAsync(x => x.EmployeeId == EmployeeId);
       

        public async Task<List<EmployeeViewModel>> GetEmployees()
        {           
          var employees=  await _unitOfWork.Employees.ToListAsync();
           return mapper.Map<List<EmployeeViewModel>>(employees);   
           

        }
      

        public async Task<bool> PostEmployee(Employee employee)
        {
          await  _unitOfWork.Employees.AddAsync(employee);
           var res= await _unitOfWork.SaveChangesAsync();
            return res==1;
        }

        public async Task<bool> PutEmployee(int EmployeeId, Employee employee)
        {
            var Emp =await _unitOfWork.Employees.SingleOrDefaultAsync(x => x.EmployeeId == EmployeeId);

            Emp.FirstName = employee.FirstName;
            Emp.LastName=employee.LastName;
            var res= await _unitOfWork.SaveChangesAsync();
            return res == 1;

        }
      
    }
}
