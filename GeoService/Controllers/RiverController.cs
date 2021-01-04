using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomeinLaag.Exceptions;
using GeoService.Interfaces;
using GeoService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GeoService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RiverController : ControllerBase
    {
        private IApiWork Api;
        private readonly ILogger Logger;
        public RiverController(IApiWork api, ILogger<ContinentController> logger)
        {
            Api = api;
            Logger = logger;
        }


        [HttpPost]
        public ActionResult<RiverDTOOut> CreateRiver([FromBody] RiverDTOIn rivier)
        {
            try
            {
                RiverDTOOut result = Api.AddRiver(rivier);
                return CreatedAtAction(nameof(CreateRiver), result);
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
        public ActionResult<RiverDTOOut> GetRiver(int id)
        {
            try
            {
                RiverDTOOut result = Api.GetRiverForId(id);
                return Ok( result);
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
        public ActionResult DeleteRiver(int id)
        {
            try
            {
                Api.DeleteRiver(id);
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

        [HttpPut]
        [Route("{id}")]
        public ActionResult<RiverDTOOut> UpdateRiver(int id, [FromBody] RiverDTOIn river)
        {
            if (river == null || river.RiverId != id)
            {
                return BadRequest("The Id for the river was not correct");
            }
            else
            {
                try
                {
                    return CreatedAtAction(nameof(UpdateRiver), Api.UpdateRiver(river));
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
