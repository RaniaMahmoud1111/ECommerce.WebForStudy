using System.Text.Json;
using Domain.Contracts;
using Domain.Models.IdentityModule;
using ECommerce.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositories;
using Services;
using Services.MappingProfiles;
using ServicesAbstractions;
using Shared.ErrorModels;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);// my exrension method 
builder.Services.AddControllers();
builder.Services.AddCors(options=>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
    });

});

//builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSWaggerServices();// my exrension method 

builder.Services.AddApplicationServices();// my exrension method (SManager,AMapper)

builder.Services.AddWebApplicationServices();
builder.Services.AddJwtService(builder.Configuration);

var app = builder.Build();


using var scope=app.Services.CreateScope();
var ObjectForDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
ObjectForDataSeeding.DataSeed();
ObjectForDataSeeding.IdentityDataSeed();

#region  Learn & use MiddleWare
// there are two ways to write middel wares 
//1. normal code (function) in class program 
#region   Way01 
//app.Use(async (RequestContext,NextMiddleWare) =>
//{
//    Console.WriteLine("Request Under Processing");
//    await NextMiddleWare.Invoke();
//    Console.WriteLine("Waiting Respone");
//    Console.WriteLine(RequestContext.Response.Body);

//});
#endregion
//2. class and use it in program that class must include:
// a public constructor with a parameter of type RequestDelegate
// a public method named Invoke or InvokeAsync this method must  1- return a Task. 2- Accept a first paramerter of type HttpContext.

#region   Way02
app.UseMiddleware<CustomExceptionHandlerMiddleware>();
#endregion
#endregion



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options=>
    {
        options.ConfigObject = new ConfigObject()
        {
            DisplayRequestDuration = true
        };
        options.DocumentTitle = "E-Commer Api";
        options.JsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy=JsonNamingPolicy.CamelCase

        };
        options.DocExpansion(DocExpansion.None);
        options.EnableFilter();
        options.EnablePersistAuthorization();

    });




}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAll");// pass policy name 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
