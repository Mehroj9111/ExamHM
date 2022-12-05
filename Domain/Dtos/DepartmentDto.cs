public class DepartmentDto{
    public int Id {get; set;}
    public string DepartmentName {get; set;}
    public int ManagerId {get; set;}
    public int LocationId {get; set;}
}
// create table Departments(
// 	id serial primary key,
// 	department_name varchar(100),
// 	manager_id int,
// 	location_id int not null references locations(id)
// );