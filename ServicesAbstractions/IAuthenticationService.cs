using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.IdentityDtos;

namespace ServicesAbstractions
{
    public  interface IAuthenticationService
    {
        //register 
        //Take Email , Password  , UserName , Display Name And Phone Number
        //Then Return Token , Email and Display Name To Client  

        Task<UserDto> RegisterAsync(RegisterDto registerDto);

        //Login
        //Take Email
        //Then Return Token , Email and Display Name To Client  

        Task<UserDto> LoginAsync(LoginDto loginDto);


        //check email 
        //Take email then return bool
        Task<bool> CheckEmailAsync(string email);

        //Get Current User Address 
        //Take email then return address of Current  Logged in user 

        Task<AddressDto> GetCurrentAddress(string email);

        //Update Current User 
        //Take updated address  and email return address  after update 

        Task<AddressDto> UpdateCurrentUserAddress(string email, AddressDto  addressDtro);

        //Get Current User 
        //Take email then return token email , Display Name 

        Task<UserDto>GetCurrentUserAsync(string email);

    }
}
