using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstractions;
using Shared.IdentityDtos;

namespace Services
{
    // to call any thing exist in app setting i neeed object of IConfiguration
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,IConfiguration _configuration,IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {

            var user = await _userManager.FindByEmailAsync(email);
           return user is not null;

        }

        public async Task<AddressDto> GetCurrentAddress(string email)
        {
            var user = _userManager.Users.Include(u => u.Address)
                 .FirstOrDefault(u => u.Email == email) ?? throw new UserNotFoundException(email);

            if(user.Address is not null)
            {
                return _mapper.Map<Address, AddressDto>(user.Address);
            }
            else
            {
                throw new AddressNotFoundException(user.DisplayName);
            }
        }

        public async  Task<UserDto> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);
            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await CreateTokenAsync(user)
            };
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            //check email is exist
            // i will use find by email that exist in userManager 
            var user=  await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) throw new UserNotFoundException(loginDto.Email);
            //check pasword 
            var password =await   _userManager.CheckPasswordAsync(user, loginDto.Password);

            if(password)
            {
                //return userDto
                return new UserDto()
                {
                    Email= loginDto.Email,
                    DisplayName=user.DisplayName,
                    Token =await  CreateTokenAsync(user)


                };

            }

            throw new UnAuthorizedException();
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            //Mapping Betweeen RegisterDto to Applicationuser
            var user = new ApplicationUser()
            { 
            DisplayName=registerDto.DisplayName,
            Email=registerDto.Email,
           PhoneNumber=registerDto.PhoneNumber,
           UserName=registerDto.UserName,
          
            };

            //Create User[Appication User]
            var result=   await _userManager.CreateAsync(user,registerDto.Password);
            if(result.Succeeded)
            {
                //Return UserDto
                return new UserDto()
                { DisplayName=user.DisplayName, Email=user.Email,Token= await CreateTokenAsync(user) };

            }
            else
            {
                //Throw Exception BadRequest 
                // result has (Succeed , errors)
                var errors = result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(errors);


            }






        }

        public async Task<AddressDto> UpdateCurrentUserAddress(string email, AddressDto addressDto)
        {
            var user = _userManager.Users.Include(u => u.Address)
                 .FirstOrDefault(u => u.Email == email) ?? throw new UserNotFoundException(email);

            if(user.Address is not null )//update
            {
                //get entity then chanage it 
                user.Address.FirstName= addressDto.FirstName;
                user.Address.LastName= addressDto.LastName;
                user.Address.City= addressDto.City;
                user.Address.Country= addressDto.Country;
                user.Address.Street= addressDto.Street;

            }
            else// add address
            {
                user.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }
            _userManager.UpdateAsync(user);
            return _mapper.Map<AddressDto>(user.Address);

        }

        private async Task<string> CreateTokenAsync(ApplicationUser user )
        {

            //header( Algo, type ) claims secret key 
            //claims :payloads  between front & back 
            var Claims = new List<Claim>()
            {
                new (ClaimTypes.Email,user.Email!),
                new (ClaimTypes.Name,user.UserName!),
                new (ClaimTypes.NameIdentifier,user.Id!)
                
            };

            var Roles = await _userManager.GetRolesAsync(user);// return list of roles 
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }


            // sec algo , claims:payloads,seckret key 
            // decode secritkey to array of bytes
            var secritKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secritKey));


            //credintial key , algo 
            var credintials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);


            var Token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credintials
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }






    }
}
