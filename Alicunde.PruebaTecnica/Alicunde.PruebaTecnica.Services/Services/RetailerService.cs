using System.Net.Http.Json;
using Alicunde.PruebaTecnica.Services.DTOs;

namespace Alicunde.PruebaTecnica.Services.Services;

public class RetailerService(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("opendata");


    public async Task<List<Retailer>> GetRetailers()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("EXP01/Retailers");
        response.EnsureSuccessStatusCode();
        List<Retailer>? retailers = await response.Content.ReadFromJsonAsync<List<Retailer>>();
        return retailers ?? [];
    }
}