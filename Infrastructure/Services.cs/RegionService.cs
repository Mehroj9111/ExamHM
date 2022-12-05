using System.Net;
using Dapper;

public class RegionService
{
    private readonly DapperContext _context;
    public RegionService(DapperContext context)
    {
        _context = context;
    }

    public async Task<Response<List<RegionDto>>> GetRegions()
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = "Select id as Id, region_name as RegionName from regions";
            var response = await connection.QueryAsync<RegionDto>(sql);
            return new Response<List<RegionDto>>(response.ToList());
        }
    }

    public async Task<Response<RegionDto>> InsertRegion(RegionDto region)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Insert into regions(region_name) values('{region.RegionName}') returning id";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                region.Id = response;
                return new Response<RegionDto>(region);
            }
        }
        catch (Exception ex)
        {
            return new Response<RegionDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<RegionDto>> UpdateRegion(RegionDto region)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Update regions set region_name = '{region.RegionName}' where id = {region.Id} returning id";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                region.Id = response;
                return new Response<RegionDto>(region);
            }
        }
        catch (Exception ex)
        {
            return new Response<RegionDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> DeleteRegion(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = $"Delete from regions where id = {id}";
            var response = await connection.ExecuteAsync(sql);
            if(response>0)
                return new Response<string>("Region deleted successfully");
                return new Response<string>(HttpStatusCode.BadRequest,"Region not found");
        }
    }

}