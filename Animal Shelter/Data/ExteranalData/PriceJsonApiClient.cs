using System.Globalization;
using Animal_Shelter.Models;
using Newtonsoft.Json;

namespace Animal_Shelter.Data.ExteranalData;

public interface IPriceJsonApiClient
{
    Task<int> GetFoodPrice(string animalTypeValue );
}

public class PriceJsonApiClient : IPriceJsonApiClient
{
    private const decimal UsdPlnValue = 3.92m;
    private string ApiKey;
    private string ApiHost = "pricejson-amazon.p.rapidapi.com";

    public PriceJsonApiClient(string apiKey)
    {
        ApiKey = apiKey;
    }

    public async Task<int> GetFoodPrice(string animalTypeValue )
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://{ApiHost}/pricejson/search?q=${animalTypeValue}%20food%2030lb&category=dog%20supplies"),
            Headers =
            {
                { "X-RapidAPI-Key", ApiKey },
                { "X-RapidAPI-Host", ApiHost }
            }
        };

        using var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return ConvertPrice(30);
        var body = await response.Content.ReadAsStringAsync();
        
        var priceResponses = JsonConvert.DeserializeObject<PriceJsonResponse>(body)
                             ?? throw new InvalidOperationException("Invalid Price Json Api Response");
        
        if (priceResponses.Products.Count == 0)
        {
            throw new InvalidOperationException("No price data found in the response");
        }

        var products = priceResponses.Products.Select(res => res.Price).ToList();
        var pricesString = products.Where(p => p != null) .Select(price => price.Replace("$", ""));
        var sumPrices = pricesString.Select(price => decimal.Parse(price, NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture)).ToList().Sum();
        var averagePrice = sumPrices / products.Count();
        return ConvertPrice(averagePrice);
    }

    private int ConvertPrice(decimal price)
    {
        var pln = price * UsdPlnValue;
        return (int)Math.Round(pln);
    } 
}