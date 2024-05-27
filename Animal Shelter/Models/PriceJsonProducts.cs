using Newtonsoft.Json;

namespace Animal_Shelter.Models;

public class PriceJsonProducts
{
    [JsonProperty("asin")]
    public string Asin { get; set; }
    [JsonProperty("image")]
    public string Image { get; set; }
    [JsonProperty("prime_eligble")]
    public bool PrimeEligible { get; set; }
    [JsonProperty("url")]
    public string Url { get; set; }
    [JsonProperty("title")]
    public string Title { get; set; }
    [JsonProperty("price")]
    public string Price { get; set; }
}