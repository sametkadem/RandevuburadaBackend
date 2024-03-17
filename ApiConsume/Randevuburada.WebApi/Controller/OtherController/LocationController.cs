using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.Location;


namespace Randevuburada.WebApi.Controller.OtherController
{
    [Route("api/v1/")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IDistrictService _districtService;
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;

        public LocationController(IDistrictService districtService, ICityService cityService, ICountryService countryService)
        {
            _districtService = districtService;
            _cityService = cityService;
            _countryService = countryService;
        }

        [HttpGet]
        [Route("admin/add/location/CityAndDistrict")]
        public async Task<IActionResult> AddCityAndDistrict()
        {
            var control = _cityService.TGetList();
            if (control.Count > 0)
            {
                var errorResponse = new
                {
                    code = 400,
                    status = "error",
                    message = "Şehirler zaten ekli!"
                };
                return BadRequest(errorResponse);
            }
            var CountryModel = new Country
            {
                CountryName = "Türkiye"
            };
            _countryService.TInsert(CountryModel);

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
            var successResponse = new
            {
                code = 200,
                status = "success",
                message = "Şehirler başarıyla eklendi!"
            };
            return Ok(successResponse);
        }

        [HttpGet]
        [Route("get/list/city")]
        public IActionResult GetCity()
        {
            var result = _cityService.TGetList();
            var successResponse = new
            {
                code = 200,
                status = "success",
                data = result
            };
            return Ok(successResponse);
        }

        [HttpGet]
        [Route("get/byId/city")]
        public IActionResult GetCity(int id)
        {
            var result = _cityService.TGetByID(id);
            if (result == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Belirtilen id ile kayıt bulunamadı!"
                };
                return NotFound(errorResponse);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                data = result
            };
            return Ok(successResponse);
        }

        [HttpGet]
        [Route("get/list/district")]
        public IActionResult GetDistrictsByCityId(int cityId)
        {
            var districts = _districtService.TGetList().Where(d => d.CityId == cityId).ToList();
            if (districts.Count == 0)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Belirtilen şehir ID'si ile ilgili hiçbir ilçe bulunamadı!"
                };
                return NotFound(errorResponse);
            }

            var returnData = new
            {
                code = 200,
                status = "success",
                data = new
                {
                    cityId = cityId,
                    districts = districts
                }
            };
            return Ok(returnData);
        }

        [HttpGet]
        [Route("get/byId/district")]
        public IActionResult GetDistrictById(int id)
        {
            var district = _districtService.TGetByID(id);
            if (district == null)
            {
                var errorResponse = new
                {
                    code = 404,
                    status = "error",
                    message = "Belirtilen id ile ilçe bulunamadı!"
                };
                return NotFound(errorResponse);
            }

            var successResponse = new
            {
                code = 200,
                status = "success",
                data = district
            };
            return Ok(successResponse);
        }

    }
}
