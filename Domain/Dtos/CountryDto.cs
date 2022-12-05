public class CountryDto{
    public int Id {get; set;}
    public int RegionId {get; set;}
    public string CountryName {get; set;}
}
// create table Countries(
// 	id serial primary key,
// 	region_id int not null references regions(id),
// 	country_name varchar(50)	
// );
