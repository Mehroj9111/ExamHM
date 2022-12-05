using System.Net;
using Dapper;

public class LocationService
{
    private readonly DapperContext _context;
    public LocationService(DapperContext context)
    {
        _context = context;
    }

    public async Task<Response<List<LocationDto>>> GetLocations()
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = "Select id as Id, street_address as StreetAddress, postal_code as PostalCode, city as City, state_province as StateProvince, country_id as CountryId from locations";
            var response = await connection.QueryAsync<LocationDto>(sql);
            return new Response<List<LocationDto>>(response.ToList());
        }
    }

    public async Task<Response<LocationDto>> InsertLocation(LocationDto location)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Insert into locations(street_address,postal_code,city,state_province,country_id) values('{location.StreetAddress}',{location.PostalCode},'{location.City}','{location.StateProvince}',{location.CountryId}) returning id";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                location.Id = response;
                return new Response<LocationDto>(location);
            }
        }
        catch (Exception ex)
        {
            return new Response<LocationDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<LocationDto>> UpdateLocation(LocationDto location)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Update locations set street_address = '{location.StreetAddress}', postal_code = {location.PostalCode}, city = '{location.City}', state_province = '{location.StateProvince}', country_id = '{location.CountryId}' where id = {location.Id} returning id";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                location.Id = response;
                return new Response<LocationDto>(location);
            }
        }
        catch (Exception ex)
        {
            return new Response<LocationDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> DeleteLocation(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = $"Delete from locations where id = {id}";
            var response = await connection.ExecuteAsync(sql);
            if(response>0)
                return new Response<string>("Location deleted successfully");
                return new Response<string>(HttpStatusCode.BadRequest,"Category not found");
        }
    }

}