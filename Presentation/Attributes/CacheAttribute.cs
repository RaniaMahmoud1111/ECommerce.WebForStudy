using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServicesAbstractions;

namespace Presentation.Attributes
{
    //  inherit Attribute and   IAsyncActionFilter  to apply OnActionExecutionAsync or inherit from ActionFilterAttribute instead 
    public class CacheAttribute(int DurationInSec =90) :ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            //create cacheKey 
            //different keys 
            //https://localhost:7159/api/products
            //https://localhost:7159/api/products?typeId=10&brandId=20
            //https://localhost:7159/api/products?brandId=20&typeId=10
            //https://localhost:7159/api/products?typeId=10

            string cacheKey = CreateCacheKey(context.HttpContext.Request);


            // search for value with cache key  check IsCached??
            //call getAsync in service
            ICacheService cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cacheValue = await cacheService.GetAsync(cacheKey);

            //return value if is not null

            if(cacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK

                };
                return;// to stop of calling the end point  as you get the cached data 
            }

            //return if is null 
          var ExcutedContent=await next.Invoke();//call the api and catch the response 
            //set value with cacheKey 
            if(ExcutedContent.Result is OkObjectResult result )//check if result returned with data ok , remember is operator check and make casting 
            {
               await cacheService.SetAsync(cacheKey, result.Value, TimeSpan.FromSeconds(DurationInSec));
            }



        }

        private string CreateCacheKey(HttpRequest request)
        {
            //https://localhost:7159/api/products?typeId=10&brandId=20
            //https://localhost:7159/api/products?brandId=20&typeId=10
            StringBuilder key=new StringBuilder();
            key.Append(request.Path + '?');
            foreach (var item in request.Query.OrderBy(q=>q.Key))
            {
                key.Append($"{item.Key}={item.Value}&");

            }

            return key.ToString();
        }
    }



}
