using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FortCode.Common;
using FortCode.Model;
using FortCode.Model.Request;
using FortCode.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FortCode.Controller
{
    [Authorize]
    [Route(Routes.AccountRoute)]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IFortService _fortService;
        public AccountController(IFortService fortService)
        {
            _fortService = fortService;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [Route(Routes.AddUserRoute)]
        public async Task<IActionResult> AddUserAsync([FromBody] AddUserRequest addUserRequest)
        {
            try
            {
                var response = await _fortService.AddUserAsync(addUserRequest);
                return response > 0 ? Ok("Success") : StatusCode(StatusCodes.Status422UnprocessableEntity,"User Creation Failed");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [Route(Routes.AddCountryRoute)]
        public async Task<IActionResult> AddCountryAsync([FromBody] AddCountryRequest addCountryRequest)
        {
            try
            {
                var claimsIdentity = User.Identity as System.Security.Claims.ClaimsIdentity;
                var Claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                var response = await _fortService.AddCountryAsync(addCountryRequest,Claim != null ? Convert.ToInt32(Claim.Value) : 0);
                return response > 0 ? Ok("Success") : StatusCode(StatusCodes.Status422UnprocessableEntity, "User Creation Failed");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [Route(Routes.GetCountryRoute)]
        public async Task<IActionResult> GetAllCountryByUserAsync()
        {
            try
            {
                var claimsIdentity = User.Identity as System.Security.Claims.ClaimsIdentity;
                var Claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                var user = await _fortService.GetAllCountryByUserAsync(Claim != null ? Convert.ToInt32(Claim.Value) : 0);

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}