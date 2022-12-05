using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class JobHistoryController{
    private JobHistoryService _jobHistoryService;
    public JobHistoryController(JobHistoryService jobHistoryService){
        _jobHistoryService = jobHistoryService;
    }

    [HttpGet]
    public async Task<Response<List<JobHistoryDto>>> GetJobHistories(){
        return await _jobHistoryService.GetJobHistories();
    }

    [HttpPost]
    public async Task<Response<JobHistoryDto>> InsertJobHistory(JobHistoryDto jobHistory){
        return await _jobHistoryService.InsertJobHistory(jobHistory);
    }
    [HttpPut]
    public async Task<Response<JobHistoryDto>> UpdateJobHistory(JobHistoryDto jobHistory){
        return await _jobHistoryService.UpdateJobHistory(jobHistory);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteJobHistory(int id){
        return await _jobHistoryService.DeleteJobHistory(id);
    }
}