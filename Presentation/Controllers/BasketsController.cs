using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.DataTransferObject.BasketModuleDtos;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // to use methodes in Basketservice i need clr to inject object of IBaseket service but in case i not deal with BasketService only but may need to deal with onthor services in sam econtroller  so ask clr to inject obj of ServiceManager 
    public class BasketsController(IServiceManager _serviceManager):ControllerBase
    {
        //Get Basket endpoint

        // ActionResult to show the shape of the result  
        [HttpGet]//Get BaseUrl/api/Basket
        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        {
            var Basket =  await _serviceManager.BasketService.GetBasketAsync(key);
            return Ok(Basket);


        }

        //CreateOrUpdate
        [HttpPost]//post BaseUrl/api/Basket
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket =await  _serviceManager.BasketService.CreateOrUpdateBasket(basket);
            return Ok(Basket);
        }
        //Delete Basket 
        [HttpDelete("{key}")]//Delete BaseUrl/api/Basket/{key}
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var Result =  await _serviceManager.BasketService.DeleteBasketAsync(key);
            return Ok(Result);
        }


    }
}
