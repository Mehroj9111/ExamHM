using System.Net;
using Dapper;
using Microsoft.AspNetCore.Hosting;

public class EmployeeService
{
    private readonly DapperContext _context;
    private readonly IWebHostEnvironment _hostEnvironment;
    public EmployeeService(DapperContext context)
    {
        _context = context;
    }
    // public EmployeeService(IWebHostEnvironment hostEnvironment)
    // {
    //     _hostEnvironment = hostEnvironment;
    // }

    public async Task<Response<List<EmployeeDto>>> GetEmployees()
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = "Select id as Id, first_name as FirstName, last_name as LastName, email as Email, phone_number as PhoneNumber, department_id as DepartmentId, manager_id as ManagerId, commission as Commission, salary as Salary, job_id as JobId, hire_date as HireDate from authors returning id";
            var response = await connection.QueryAsync<EmployeeDto>(sql);
            return new Response<List<EmployeeDto>>(response.ToList());
        }
    }

    public async Task<Response<EmployeeDto>> InsertEmployee(EmployeeDto employee)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                // var path = Path.Combine(_hostEnvironment.WebRootPath, "ProfileImages");
                // if (Directory.Exists(path) == false)
                // {
                //     Directory.CreateDirectory(path);
                // }
                // var filePath = Path.Combine(path, employee.File.FileName);
                // using (var stream = File.Create(filePath))
                // {
                //     await employee.File.CopyToAsync(stream);
                // }
                // var sql = $"Insert into employees(first_name,last_name,email,phone_number,department_id,manager_id,commission,salary,job_id,hire_date,file_name) values('{employee.FirstName}','{employee.LastName}','{employee.Email}',{employee.PhoneNumber},{employee.DepartmentId},{employee.ManagerId},{employee.Commission},{employee.Salary},{employee.JobId},'{employee.HireDate}','{employee.File.FileName}') returning id";
                var sql = $"Insert into employees(first_name,last_name,email,phone_number,department_id,manager_id,commission,salary,job_id,hire_date) values('{employee.FirstName}','{employee.LastName}','{employee.Email}',{employee.PhoneNumber},{employee.DepartmentId},{employee.ManagerId},{employee.Commission},{employee.Salary},{employee.JobId},'{employee.HireDate}') returning id";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                employee.Id = response;
                return new Response<EmployeeDto>(employee);
            }
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<EmployeeDto>> UpdateEmployee(EmployeeDto employee)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Update employees set first_name = '{employee.FirstName}', last_name = '{employee.LastName}', email = '{employee.Email}', phone_number = {employee.PhoneNumber}, department_id = {employee.DepartmentId}, manager_id = {employee.ManagerId}, commission = {employee.Commission}, salary = {employee.Salary}, job_id = '{employee.JobId}', hire_date = '{employee.HireDate}' where id = {employee.Id} returining id";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                employee.Id = response;
                return new Response<EmployeeDto>(employee);
            }
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> DeleteEmployee(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = $"Delete from employees where id = {id}";
            var response = await connection.ExecuteAsync(sql);
            if (response > 0)
                return new Response<string>("Employee deleted successfully");
            return new Response<string>(HttpStatusCode.BadRequest, "Category not found");
        }
    }

}