using CORE_Rest_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_Rest_MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository ProfileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            ProfileRepository = profileRepository;
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Profile>> GetProfileById(int id)
        {
            try
            {
                //var identity = HttpContext.User.Identity as ClaimsIdentity;
                //if (identity != null)
                //{
                //    IEnumerable<Claim> claims = identity.Claims;
                //    // or
                //    var iden = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var result = await ProfileRepository.GetProfile(id);
                    if (result == null)
                    {
                        return NotFound();
                    }
                    return result;

                
                //else
                //{
                //    return Unauthorized("Token not valid");
                //}

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Profile>> UpdateProfilee([FromForm] Profile profile,int id)
        {
            try
            {
                //var identity = HttpContext.User.Identity as ClaimsIdentity;
                //if (identity != null)
                //{
                //    IEnumerable<Claim> claims = identity.Claims;
                //    // or
                //    var iden = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    if (id != profile.ProfileId)
                    {
                        return BadRequest("Profile id mismatch");
                    }

                    var empUpdate = await ProfileRepository.GetProfile(id);
                    if (empUpdate == null)
                    {
                        return NotFound($"Profile with id {id} not found");
                    }
                    return await ProfileRepository.UpdateProfile(profile);
                //}
                //else
                //{
                //    return Unauthorized("Token not valid");
                //}



            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating employee record" + e);
            }
        }
    }
}
