using System.Net.Http.Json;
using Alicunde.PruebaTecnica.Database.Models;
using Alicunde.PruebaTecnica.Services.DTOs;
using Alicunde.PruebaTecnica.Services.Exceptions;
using Alicunde.PruebaTecnica.Services.Repositories;

namespace Alicunde.PruebaTecnica.Services.Services;

public class RetailerService(IHttpClientFactory httpClientFactory, RetailerRepository retailerRepository)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("opendata");

    /// <summary>
    /// This method fetches the retailers from the API and stores them in the database
    /// </summary>
    public async Task FetchRetailers()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("EXP01/Retailers");
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidRemoteResponseException();
        }
        List<RetailerDto>? retailers = await response.Content.ReadFromJsonAsync<List<RetailerDto>>();
        if (retailers is null)
        {
            return;
        }


        List<RetailerDto> uniqueRetailers = retailers.DistinctBy(r => r.ReCode).ToList();
        await retailerRepository.Add(uniqueRetailers);
    }


    /// <summary>
    /// This method fetches a single retailer from the API and stores it in the database
    /// </summary>
    /// <param name="reCode">The retailer code to search for</param>
    /// <returns>The retailer if found, null otherwise</returns>
    public async Task<RetailerDto?> Find(string reCode)
    {
        Retailer? retailer = await retailerRepository.Find(reCode);
        if (retailer is null)
        {
            return null;
        }

        return new RetailerDto
        {
            ReName = retailer.ReName,
            Country = retailer.Country,
            CodingScheme = retailer.CodingScheme,
            ReCode = retailer.ReCode
        };
    }
}