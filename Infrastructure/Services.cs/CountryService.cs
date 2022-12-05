using System.Net;
using Dapper;

public class CountryService
{
    private readonly DapperContext _context;
    public CountryService(DapperContext context)
    {
        _context = context;
    }

    public async Task<Response<List<CountryDto>>> GetCountries()
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = "Select id as Id, region_id as RegionId, country_name as CountryName from countries";
            var response = await connection.QueryAsync<CountryDto>(sql);
            return new Response<List<CountryDto>>(response.ToList());
        }
    }

    public async Task<Response<CountryDto>> InsertCountry(CountryDto country)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Insert into countries(region_id,country_name) values({country.RegionId},'{country.CountryName}') returning id";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                country.Id = response;
                return new Response<CountryDto>(country);
            }
        }
        catch (Exception ex)
        {
            return new Response<CountryDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<CountryDto>> UpdateCountry(CountryDto country)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Update countries set region_id = {country.RegionId}, country_name = '{country.CountryName}' where id = {country.Id} returning id";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                country.Id = response;
                return new Response<CountryDto>(country);
            }
        }
        catch (Exception ex)
        {
            return new Response<CountryDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> DeleteCountry(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = $"Delete from countries where id = {id}";
            var response = await connection.ExecuteAsync(sql);
            if(response>0)
                return new Response<string>("Country deleted successfully");
                return new Response<string>(HttpStatusCode.BadRequest,"Country not found");
        }
    }

}