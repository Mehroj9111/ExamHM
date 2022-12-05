using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class RegionController{
    private RegionService _regionService;
    public RegionController(RegionService regionService){
        _regionService = regionService;
    }

    [HttpGet("GetRegions")]
    public async Task<Response<List<RegionDto>>> GetRegions(){
        return await _regionService.GetRegions();
    }

    [HttpPost("InsertRegion")]
    public async Task<Response<RegionDto>> InsertRegion(RegionDto region){
        return await _regionService.InsertRegion(region);
    }
    [HttpPut("UpdateRegion")]
    public async Task<Response<RegionDto>> UpdateRegion(RegionDto region){
        return await _regionService.UpdateRegion(region);
    }
    [HttpDelete("DeleteRegion")]
    public async Task<Response<string>> DeleteRegion(int id){
        return await _regionService.DeleteRegion(id);
    }
}