using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Bananagrams.Service.Dtos.DailyWords;

public class JsonResultDto
{
    // [JsonProperty("description")]
    // public string Description { get; set; }
    // [JsonProperty("imageurl")]
    // public string ImageLocation { get; set; }
    // [JsonProperty("tfvname")]
    // public string Title { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }
    [JsonProperty("imageurl")]
    public string ImageUrl { get; set; }
    [JsonProperty("tfvname")]
    public string Tfvname { get; set; }
}

public class JsonResponseDto
{
    [JsonProperty("results")]
    public JsonResultDto[] Results { get; set; }
    [JsonProperty("tfvcount")]
    public string Tfvcount { get; set; }
}