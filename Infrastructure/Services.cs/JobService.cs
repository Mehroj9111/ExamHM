using System.Net;
using Dapper;

public class JobService
{
    private readonly DapperContext _context;
    public JobService(DapperContext context)
    {
        _context = context;
    }

    public async Task<Response<List<JobDto>>> GetJobs()
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = "Select id as Id, job_title as JobTitle, min_salary as Minsalary, max_salary as MaxSalary from jobs";
            var response = await connection.QueryAsync<JobDto>(sql);
            return new Response<List<JobDto>>(response.ToList());
        }
    }

    public async Task<Response<JobDto>> InsertJob(JobDto job)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Insert into jobs(job_title,min_salary,max_salary) values('{job.JobTitle}',{job.MinSalary},{job.MaxSalary}) returning id";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                job.Id = response;
                return new Response<JobDto>(job);
            }
        }
        catch (Exception ex)
        {
            return new Response<JobDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<JobDto>> UpdateJob(JobDto job)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Update jobs set job_title = '{job.JobTitle}', min_salary = {job.MinSalary}, max_salary = {job.MaxSalary} where id = {job.Id} returning id";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                job.Id = response;
                return new Response<JobDto>(job);
            }
        }
        catch (Exception ex)
        {
            return new Response<JobDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> DeleteJob(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = $"Delete from jobs where id = {id}";
            var response = await connection.ExecuteAsync(sql);
            if(response>0)
                return new Response<string>("Job deleted successfully");
                return new Response<string>(HttpStatusCode.BadRequest,"Job not found");
        }
    }

}