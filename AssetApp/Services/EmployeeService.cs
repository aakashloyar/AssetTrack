using AssetApp.Models;
using AssetApp.SqlQueries;

namespace AssetApp.Services
{
    public class EmployeeService
    {
        private readonly DatabaseService _dbService;

        public EmployeeService(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        // Create Table (if not exists)
        public async Task CreateTableIfNotExistsAsync()
        {
            await _dbService.ExecuteAsync(EmployeeQueries.CreateTable);
        }

        // Get all employees
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _dbService.QueryAsync<Employee>(EmployeeQueries.GetAllEmployees);
        }

        //  Get employee by ID
        public async Task<Employee?> GetByIdAsync(int employeeId)
        {
            return await _dbService.QuerySingleAsync<Employee>(
                EmployeeQueries.GetEmployeeById,
                new { EmployeeID = employeeId }
            );
        }

        //  Add new employee
        public async Task<int> AddAsync(Employee employee)
        {
            // Insert and return new employee ID (SCOPE_IDENTITY)
            var sql = EmployeeQueries.AddEmployee;
            return await _dbService.ExecuteScalarAsync<int>(sql, employee);
        }

        //  Update existing employee
        public async Task<bool> UpdateAsync(Employee employee)
        {
            var rowsAffected = await _dbService.ExecuteAsync(EmployeeQueries.UpdateEmployee, employee);
            return rowsAffected > 0;
        }

        //  Delete employee by ID
        public async Task<bool> DeleteAsync(int employeeId)
        {
            var rowsAffected = await _dbService.ExecuteAsync(
                EmployeeQueries.DeleteEmployee,
                new { EmployeeID = employeeId }
            );
            return rowsAffected > 0;
        }
        
        public async Task<IEnumerable<Employee>> GetFilteredEmployeesAsync(
            string? fullName,
            string? department,
            string? email,
            string? phoneNumber)
        {
            var sql = EmployeeQueries.GetFilteredEmployees(fullName, department, email, phoneNumber);

            var employees = await _dbService.QueryAsync<Employee>(sql, new
            {
                FullName = fullName,
                Department = department,
                Email = email,
                PhoneNumber = phoneNumber
            });

            return employees;
        }
    }
}
