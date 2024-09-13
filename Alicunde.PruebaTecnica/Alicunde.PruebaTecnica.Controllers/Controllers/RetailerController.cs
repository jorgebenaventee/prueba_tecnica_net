using Alicunde.PruebaTecnica.Services.DTOs;
using Alicunde.PruebaTecnica.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alicunde.PruebaTecnica.Controllers.Controllers;

[ApiController, Route("api/[controller]")]
public class RetailerController(RetailerService retailerService) : ControllerBase
{
    [HttpPost("fetch-retailers")]
    public async Task<IActionResult> FetchRetailers()
    {
        await retailerService.FetchRetailers();
        return NoContent();
    }

    [HttpGet("{reCode}")]
    public async Task<IActionResult> GetRetailer(string reCode)
    {
        RetailerDto? retailer = await retailerService.Find(reCode);
        if (retailer is null)
        {
            return NotFound();
        }

        return Ok(retailer);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllRetailers()
    {
        List<RetailerDto> retailers = await retailerService.GetAll();
        return Ok(retailers);
    }
}