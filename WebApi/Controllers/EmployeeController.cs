using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class EmployeeController{
    private EmployeeService _employeeService;
    public EmployeeController(EmployeeService employeeService){
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<Response<List<EmployeeDto>>> GetEmployees(){
        return await _employeeService.GetEmployees();
    }

    [HttpPost]
    // public async Task<Response<EmployeeDto>> InsertEmployee([FromForm]EmployeeDto employee){
    //     return await _employeeService.InsertEmployee(employee);
    // }
    public async Task<Response<EmployeeDto>> InsertEmployee(EmployeeDto employee){
        return await _employeeService.InsertEmployee(employee);
    }
    [HttpPut]
    public async Task<Response<EmployeeDto>> UpdateEmployee(EmployeeDto employee){
        return await _employeeService.UpdateEmployee(employee);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteEmployee(int id){
        return await _employeeService.DeleteEmployee(id);
    }
}