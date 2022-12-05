using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class CountryController{
    private CountryService _countryService;
    public CountryController(CountryService countryService){
        _countryService = countryService;
    }

    [HttpGet]
    public async Task<Response<List<CountryDto>>> GetCountries(){
        return await _countryService.GetCountries();
    }

    [HttpPost]
    public async Task<Response<CountryDto>> InsertCountry(CountryDto country){
        return await _countryService.InsertCountry(country);
    }
    [HttpPut]
    public async Task<Response<CountryDto>> UpdateCountry(CountryDto country){
        return await _countryService.UpdateCountry(country);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteCountry(int id){
        return await _countryService.DeleteCountry(id);
    }
}