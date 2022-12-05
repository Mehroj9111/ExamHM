public class JobHistoryDto{
    public int EmployeeId {get; set;}
    public DateTime StartDate {get; set;}
    public DateTime EndDate {get; set;}
    public int JobId {get; set;}
    public int DepartmentId {get; set;}
}
// create table Job_Histories(
// 	employee_id int not null references Employees(id),
// 	start_date date,
// 	end_date date,
// 	job_id int not null references jobs(id),
// 	department_id int not null references departments(id)
// );
