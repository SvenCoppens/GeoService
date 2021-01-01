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
        private IApiWork Api;
        public ContinentController(IApiWork api)
        {
            Api = api;
        }

        [HttpPost]
        public ActionResult<ContinentDTOIn> MaakContinent([FromBody] ContinentDTOIn continent)
        {
            try
            {
                ContinentDTOOut result = Api.VoegContinentToe(continent);
                return CreatedAtAction(nameof(MaakContinent), result);
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
        public ActionResult<ContinentDTOIn> GeefContinent(int id)
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
        public ActionResult<ContinentDTOIn> UpdateContinent(int id, [FromBody] ContinentDTOIn continent)
        {
            if (continent == null || continent.ContinentId != id)
            {
                return BadRequest();
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
        public ActionResult VerwijderContinent(int id)
        {
            try
            {
                Api.VerwijderContinent(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("{id}/Country")]
        [HttpPost]
        public ActionResult<ContinentDTOOut> MaakCountry(int continentId,[FromBody] CountryDTOIn country)
        {
            try
            {
                CountryDTOOut result = Api.VoegLandToe(country);
                return CreatedAtAction(nameof(MaakContinent), result);
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
        [Route("{id}/Country/{countryId}")]
        public ActionResult<CountryDTOOut> GeefCountry(int id, int countryId)
        {
            //hier nog de id controles moeten doen
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
        public ActionResult<CountryDTOOut> VerwijderCountry(int id, int countryId)
        {
            //hier nog de id controles moeten doen
            try
            {
                Api.VerwijderCountry(countryId);
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
        [Route("{id}/Country/{countryId}")]
        public ActionResult<CountryDTOOut> UpdateCountry(int id, int countryId,[FromBody] CountryDTOIn countryIn)
        {
            //hier de checks verbeteren
            if (countryIn == null || countryIn.CountryId!= id)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    return CreatedAtAction(nameof(UpdateContinent), Api.UpdateCountry(countryIn));
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
        [Route("{id}/Country/{countryId}/City")]
        [HttpPost]
        public ActionResult<ContinentDTOOut> MaakCity(int id,int countryId,[FromBody] CityDTOIn city)
        {
            try
            {
                CityDTOOut result = Api.VoegStadToe(city);
                return CreatedAtAction(nameof(MaakCity), result);
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
        [Route("{id}/country/{countryId}/city/{cityId}")]
        [HttpGet]
        public ActionResult<ContinentDTOOut> GeefCity(int id, int countryId, int cityId)
        {
            try
            {
                CityDTOOut result = Api.GetCityForId(cityId);
                return CreatedAtAction(nameof(GeefCity), result);
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
        [HttpPost]
        public ActionResult<ContinentDTOOut> DeleteCity(int id, int countryId, int cityId)
        {
            try
            {
                Api.VerwijderCity(cityId);
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
            if (city == null || city.CityId!= id)
            {
                return BadRequest();
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
