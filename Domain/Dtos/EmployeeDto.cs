using Microsoft.AspNetCore.Http;
public class EmployeeDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int PhoneNumber { get; set; }
    public int DepartmentId { get; set; }
    public int ManagerId { get; set; }
    public double Commission { get; set; }
    public double Salary { get; set; }
    public int JobId { get; set; }
    public DateTime HireDate { get; set; }
    // public IFormFile File { get; set; }
    // public string FileName { get; set; }

}
// create table Employees(
// 	id serial primary key,
// 	first_name varchar(100),
// 	last_name varchar(100),
// 	email varchar(50) unique,
// 	phone_number int,
// 	department_id int not null references departments(id),
// 	manager_id int references employees(id),
// 	commission decimal,
// 	salary decimal,
// 	job_id int not null references jobs(id),
// 	hire_date date
// );

// alter table employees
// add file_name varchar(100);