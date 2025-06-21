using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.IdentityDtos;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public  class AuthenticationController(IServiceManager _serviceManager):ControllerBase
    {

        // login endpoit 
        [HttpPost("Login")]// POST BaseUrl/apiAuthentication/Login

        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {

          var user=  await  _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(user);
        
        }

        //Register endpoint 
        [HttpPost("Register")]//POST BaseUrl/apiAuthentication/Register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _serviceManager.AuthenticationService.RegisterAsync(registerDto);

            return Ok(user);

        }


        // check Email
        [HttpGet("CheckEmail")] // Get BaseUrl / api/Authentication/CheckEmail
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var Result =await _serviceManager.AuthenticationService.CheckEmailAsync(email);
    
            return Ok(Result);
        }

        //Get Current User
        [Authorize]
        [HttpGet("GetCurrentUser")] //Get BaseUrl/api/Authentication/GetCurrentUser
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            // it must take email or you can get email from claims:payloads
            // so we can extract email from token 

            var email = User.FindFirst(ClaimTypes.Email).Value;
            var user=await _serviceManager.AuthenticationService.GetCurrentUserAsync(email);

            return Ok(user);

        }

        //Get Current User Address endpoint 
        [Authorize]
        [HttpGet("Address")] // Get  BaseUrl/api/Authentication/Address
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var address = await _serviceManager.AuthenticationService.GetCurrentAddress(email);

            return Ok(address);
        }

        //Update Current User Address
        [Authorize]
        [HttpPut] // Put BaseUrl/api/Authentication/address
        public async Task<ActionResult<AddressDto>>UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var email=User.FindFirst(ClaimTypes.Email).Value;
            var updatedAddress = await _serviceManager.AuthenticationService.UpdateCurrentUserAddress(email,addressDto);

            return Ok(updatedAddress);
        }

    }
}
