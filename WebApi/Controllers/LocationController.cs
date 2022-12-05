using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class LocationController{
    private LocationService _locationService;
    public LocationController(LocationService locationService){
        _locationService = locationService;
    }

    [HttpGet]
    public async Task<Response<List<LocationDto>>> GetLocations(){
        return await _locationService.GetLocations();
    }

    [HttpPost]
    public async Task<Response<LocationDto>> InsertLocation(LocationDto location){
        return await _locationService.InsertLocation(location);
    }
    [HttpPut]
    public async Task<Response<LocationDto>> UpdateLocation(LocationDto location){
        return await _locationService.UpdateLocation(location);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteLocation(int id){
        return await _locationService.DeleteLocation(id);
    }
}