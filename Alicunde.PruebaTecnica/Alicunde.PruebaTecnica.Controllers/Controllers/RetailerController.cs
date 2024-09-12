using Alicunde.PruebaTecnica.Services.DTOs;
using Alicunde.PruebaTecnica.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alicunde.PruebaTecnica.Controllers.Controllers;

[ApiController, Route("api/[controller]")]
public class RetailerController(RetailerService retailerService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRetailers()
    {
        List<Retailer> retailers = await retailerService.GetRetailers();
        return Ok(retailers);
    }
}