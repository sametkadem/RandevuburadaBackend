using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.Location;


namespace Randevuburada.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IDistrictService _districtService;
        private readonly ICityService _cityService;

        public LocationController(IDistrictService districtService, ICityService cityService)
        {
            _districtService = districtService;
            _cityService = cityService;
        }

        [HttpGet]
        [Route("AddCityAndDistrict")]
        public async Task<IActionResult> AddCityAndDistrict()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://turkiyeapi.dev/api/v1/provinces");
            string content = await response.Content.ReadAsStringAsync();

            JObject jsonObject = JObject.Parse(content);
            JArray dataArray = (JArray)jsonObject["data"];
            System.Diagnostics.Debug.WriteLine(dataArray);

            foreach (var city in dataArray)
            {
                System.Diagnostics.Debug.WriteLine(city["name"]);
                var cityModel = new City
                {
                    CountryId = 1,
                    CityName = city["name"].ToString(),
                };
                _cityService.TInsert(cityModel);

                foreach (var district in city["districts"])
                {
                    var districtModel = new District
                    {
                        CityId = cityModel.Id,
                        CountryId = 1,
                        DistrictName = district["name"].ToString()
                    };
                   _districtService.TUpdate(districtModel);

                    System.Diagnostics.Debug.WriteLine(district["name"]);
                }
            }
            return Ok(content);
        }

        [HttpGet]
        [Route("GetCity")]
        public IActionResult GetCity()
        {
            var result = _cityService.TGetList();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetDistrictsByCityId")]
        public IActionResult GetDistrictsByCityId(int cityId)
        {
            var districts = _districtService.TGetList().Where(d => d.CityId == cityId).ToList();
            return Ok(districts);
        }

    }
}
