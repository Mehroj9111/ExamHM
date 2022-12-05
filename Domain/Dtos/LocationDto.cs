public class LocationDto{
    public int Id {get; set;}
    public string StreetAddress {get; set;}
    public int PostalCode {get; set;}
    public string City {get; set;}
    public string StateProvince {get; set;}
    public int CountryId {get; set;}
}
// create table Locations(
// 	id serial primary key,
// 	street_address varchar(100),
// 	postal_code int,
// 	city varchar(50),
// 	state_province varchar(100),
// 	country_id int not null references countries(id)
// );