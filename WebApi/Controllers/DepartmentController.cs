using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class DepartmentController{
    private DepartmentService _departmentService;
    public DepartmentController(DepartmentService departmentService){
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<Response<List<DepartmentDto>>> GetDepartments(){
        return await _departmentService.GetDepartments();
    }

    [HttpPost]
    public async Task<Response<DepartmentDto>> InsertDepartment(DepartmentDto department){
        return await _departmentService.InsertDepartment(department);
    }
    [HttpPut]
    public async Task<Response<DepartmentDto>> UpdateDepartment(DepartmentDto department){
        return await _departmentService.UpdateDepartment(department);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteDepartment(int id){
        return await _departmentService.DeleteDepartment(id);
    }
}