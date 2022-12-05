using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class JobController{
    private JobService _jobService;
    public JobController(JobService jobService){
        _jobService = jobService;
    }

    [HttpGet]
    public async Task<Response<List<JobDto>>> GetJobs(){
        return await _jobService.GetJobs();
    }

    [HttpPost]
    public async Task<Response<JobDto>> InsertJob(JobDto job){
        return await _jobService.InsertJob(job);
    }
    [HttpPut]
    public async Task<Response<JobDto>> UpdateJob(JobDto job){
        return await _jobService.UpdateJob(job);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteJob(int id){
        return await _jobService.DeleteJob(id);
    }
}