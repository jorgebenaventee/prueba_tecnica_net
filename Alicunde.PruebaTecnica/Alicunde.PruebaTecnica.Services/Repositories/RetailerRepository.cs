using Alicunde.PruebaTecnica.Database;
using Alicunde.PruebaTecnica.Database.Models;
using Alicunde.PruebaTecnica.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Alicunde.PruebaTecnica.Services.Repositories;

public class RetailerRepository(RetailersContext context)
{
    /// <summary>
    /// This method adds a list of retailers to the database
    /// </summary>
    /// <param name="retailerDtos">The list of retailers to add</param>
    public async Task Add(List<RetailerDto> retailerDtos)
    {
        if (retailerDtos is not { Count: > 0 })
        {
            return;
        }

        List<string> currentRetailerCodes = await context.Retailers.Select(r => r.ReCode).ToListAsync();

        List<Retailer> retailers = retailerDtos
            .Where(dto => !currentRetailerCodes.Contains(dto.ReCode))
            .Select(dto =>
                new Retailer
                {
                    ReCode = dto.ReCode,
                    ReName = dto.ReName,
                    Country = dto.Country,
                    CodingScheme = dto.CodingScheme
                })
            .ToList();
        await context.Retailers.AddRangeAsync(retailers);
        await context.SaveChangesAsync();
    }


    /// <summary>
    /// This method finds a single retailer in the database
    /// </summary>
    /// <param name="reCode">The retailer code to search for</param>
    /// <returns>The retailer if found, null otherwise</returns>
    public async Task<Retailer?> Find(string reCode)
    {
        Retailer? retailer = await context.Retailers.FirstOrDefaultAsync(r => r.ReCode == reCode);
        return retailer;
    }
    
    /// <summary>
    /// This method fetches all the retailers from the database
    /// </summary>
    /// <returns>A list of retailers</returns>
    public async Task<List<Retailer>> GetAll()
    {
        return await context.Retailers.ToListAsync();
    }
}