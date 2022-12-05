using System.Net;
using Dapper;

public class JobHistoryService
{
    private readonly DapperContext _context;
    public JobHistoryService(DapperContext context)
    {
        _context = context;
    }

    public async Task<Response<List<JobHistoryDto>>> GetJobHistories()
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = "Select employee_id as EmployeeId, start_date as StartDate, end_date as EndDate, job_id as JobId, department_id as DepartmentId from Job_histories";
            var response = await connection.QueryAsync<JobHistoryDto>(sql);
            return new Response<List<JobHistoryDto>>(response.ToList());
        }
    }

    public async Task<Response<JobHistoryDto>> InsertJobHistory(JobHistoryDto jobHistory)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Insert into job_histories(start_date,end_date,job_id,department_id) values('{jobHistory.StartDate}','{jobHistory.EndDate}',{jobHistory.JobId},{jobHistory.DepartmentId}) returning employee_id";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                jobHistory.EmployeeId = response;
                return new Response<JobHistoryDto>(jobHistory);
            }
        }
        catch (Exception ex)
        {
            return new Response<JobHistoryDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<JobHistoryDto>> UpdateJobHistory(JobHistoryDto jobHistory)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Update job_histories set start_date = '{jobHistory.StartDate}', end_date = '{jobHistory.EndDate}', job_id = {jobHistory.JobId}, department_id = {jobHistory.DepartmentId} where id = {jobHistory.EmployeeId} returning employee_id";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                jobHistory.EmployeeId = response;
                return new Response<JobHistoryDto>(jobHistory);
            }
        }
        catch (Exception ex)
        {
            return new Response<JobHistoryDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> DeleteJobHistory(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = $"Delete from job_histories where employee_id = {id}";
            var response = await connection.ExecuteAsync(sql);
            if(response>0)
                return new Response<string>("Job History deleted successfully");
                return new Response<string>(HttpStatusCode.BadRequest,"Job History not found");
        }
    }

}