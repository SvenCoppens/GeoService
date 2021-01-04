using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomeinLaag.Exceptions;
using GeoService.Interfaces;
using GeoService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GeoService.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ContinentController : ControllerBase
    {
        private readonly ILogger Logger;
        private IApiWork Api;
        public ContinentController(IApiWork api,ILogger<ContinentController> logger)
        {
            Api = api;
            Logger = logger;
        }

        [HttpPost]
        public ActionResult<ContinentDTOOut> CreateContinent([FromBody] ContinentDTOIn continent)
        {
            //Logger.LogInformation(DateTime.Now + $" - CreateContinent Called.");
            try
            {
                ContinentDTOOut result = Api.AddContinent(continent);
                return CreatedAtAction(nameof(CreateContinent), result);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ContinentDTOOut> GetContinent(int id)
        {
            try
            {
                return Ok(Api.GetContinentForId(id));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<ContinentDTOOut> UpdateContinent(int id, [FromBody] ContinentDTOIn continent)
        {
            if (continent.ContinentId != id)
            {
                return BadRequest("The ContinentId did not match or was not correct");
            }
            else
            {
                try
                {
                    return CreatedAtAction(nameof(UpdateContinent), Api.UpdateContinent(continent));
                }
                catch (DomainException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteContinent(int id)
        {
            try
            {
                Api.DeleteContinent(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        [Route("{continentId}/Country")]
        [HttpPost]
        public ActionResult<CountryDTOOut> CreateCountry(int continentId, [FromBody] CountryDTOIn country)
        {
            if (country.ContinentId != continentId)
                return BadRequest("The ContinentIds did not match.");
            else
            {
                try
                {
                    CountryDTOOut result = Api.AddCountry(country);
                    return CreatedAtAction(nameof(CreateContinent), result);
                }
                catch (DomainException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }
        [HttpGet]
        [Route("{id}/Country/{countryId}")]
        public ActionResult<CountryDTOOut> GetCountry(int id, int countryId)
        {

            try
            {
                return Ok(Api.GetCountryForId(countryId));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
        [HttpDelete]
        [Route("{id}/Country/{countryId}")]
        public ActionResult<CountryDTOOut> DeleteCountry(int id, int countryId)
        {
            
            try
            {
                Api.DeleteCountry(countryId);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [Route("{ContinentId}/Country/{countryId}")]
        public ActionResult<CountryDTOOut> UpdateCountry(int ContinentId, int countryId, [FromBody] CountryDTOIn countryIn)
        {
            //hier de checks verbeteren
            if (countryIn.CountryId != countryId)
            {
                return BadRequest("The CountryIds dit not match or were not correct.");
            }
            else
            {
                try
                {
                    return CreatedAtAction(nameof(UpdateCountry), Api.UpdateCountry(countryIn));
                }
                catch (DomainException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }
        [Route("{ContinentId}/Country/{countryId}/City")]
        [HttpPost]
        public ActionResult<ContinentDTOOut> CreateCity(int ContinentId, int countryId, [FromBody] CityDTOIn city)
        {
            if (city.CountryId!=countryId)
            {
                return BadRequest("The CountryIds dit not match or were not correct.");
            }
            else
            {
                try
                {
                    CityDTOOut result = Api.AddCity(city);
                    return CreatedAtAction(nameof(CreateCity), result);
                }
                catch (DomainException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }
        [Route("{id}/country/{countryId}/city/{cityId}")]
        [HttpGet]
        public ActionResult<ContinentDTOOut> GetCity(int id, int countryId, int cityId)
        {
            try
            {
                CityDTOOut result = Api.GetCityForId(cityId);
                return Ok(result);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Route("{id}/Country/{countryId}/City/{cityId}")]
        [HttpDelete]
        public ActionResult<ContinentDTOOut> DeleteCity(int id, int countryId, int cityId)
        {
            
            try
            {
                Api.DeleteCity(cityId);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id}/Country/{countryId}/City/{cityId}")]
        public ActionResult<CountryDTOOut> UpdateCity(int id, int countryId, int cityId, [FromBody] CityDTOIn city)
        {
            //hier de checks verbeteren
            if (city.CityId != cityId)
            {
                return BadRequest("The CityIds did not match or were not correct.");
            }
            else
            {
                try
                {
                    return CreatedAtAction(nameof(UpdateCity), Api.UpdateCity(city));
                }
                catch (DomainException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }
    }
}
