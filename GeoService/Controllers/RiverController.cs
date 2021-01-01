using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomeinLaag.Exceptions;
using GeoService.Interfaces;
using GeoService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RiverController : ControllerBase
    {
        private IApiWork Api;
        public RiverController(IApiWork api)
        {
            Api = api;
        }


        [HttpPost]
        public ActionResult<RiverDTOOut> MaakRivier([FromBody] RiverDTOIn rivier)
        {
            try
            {
                RiverDTOOut result = Api.AddRiver(rivier);
                return CreatedAtAction(nameof(MaakRivier), result);
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
        public ActionResult<RiverDTOOut> GetRivier(int id)
        {
            try
            {
                RiverDTOOut result = Api.GetRiverForId(id);
                return CreatedAtAction(nameof(GetRivier), result);
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
        [Route("{id}")]
        public ActionResult VerwijderRivier(int id)
        {
            try
            {
                Api.DeleteRiver(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<RiverDTOOut> UpdateKlant(int id, [FromBody] RiverDTOIn river)
        {
            if (river == null || river.RiverId != id)
            {
                return BadRequest("The Id for the river was not correct");
            }
            else
            {
                try
                {
                    return CreatedAtAction(nameof(UpdateKlant), Api.UpdateRivier(river));
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
