using Newtonsoft.Json;

namespace Animal_Shelter.Models;

public class PriceJsonResponse
{
    [JsonProperty("q")]
    public string Query { get; set; }
    [JsonProperty("items")]
    public int ItemsCount { get; set; }
    [JsonProperty("products")]
    public List<PriceJsonProducts> Products  { get; set; }
}