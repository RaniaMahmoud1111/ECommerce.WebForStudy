using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicesAbstractions;
using Shared;
using Shared.DataTransferObject.ProductModuleDtos;
using Microsoft.AspNetCore.Authorization;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    //controller => service => unitOfWork => GenericRepo => repo =>Model 

    // i make controller to call and use services 
    //controllers contain all endpoints 
    // that controller may need clr to inject obj of more than one service but that way not prefer so we will make serviceManager
    // so now we not need to ask clr for obj for each service not i just ask for one obj of type IServiceManager 
    // to make that api controller it must inhert from ControllerBase & has attripbute [apiControllor]

    // when i use endpoint of GetAllProducts
    //service manager => product service (Get allPRoduct async that use GetAllSync in repo)=> unitof work(GetRepo) =>Generic repo =>  ProductDto
   [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController(IServiceManager _serviceManager):ControllerBase
    {
        // to show it in sweager we use ActionResult as retuen type of endpoint not IActionResult in MVC
        //our endpoint 


        //Get All Products
        [HttpGet]
        [Cache]
        //[Authorize(Roles ="Admin")]
        //Get BaseUrl/api/Products
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParameters parameters )
        
        {
          
            var Products = await _serviceManager.productService.GetAllProductsAsync(parameters);
            return Ok(Products);//obj result ok of status code 200
        }


        //Get AProduct By Id
        [HttpGet("{id:int}")]// here we put segement id 
         //Get BaseUrl/api/Products
         public async Task<ActionResult<ProductDto>> GetProductById(int id)
         {
            var Product = await _serviceManager.productService.GetProductByIdAsync(id);
            return Ok(Product);
         }


       //Get All Brands
        [HttpGet("Brands")]// we put segement to differ from other endpoints 
        [Cache]
       //GEt BaseUrl/api/Products/Brands
       public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var Brands = await _serviceManager.productService.GetAllBrandsAsync();
            return Ok(Brands);
        }


        //Get All Types
        [HttpGet("Types")]
        [Cache]
        // Get BaseUrl/api/Products/Types
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var Types =await  _serviceManager.productService.GetAllTypesAsync();
            return Ok(Types);
        }


    }



}
