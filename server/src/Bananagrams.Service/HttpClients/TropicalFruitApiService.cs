using System.Net.Http.Json;
using System.Text.Json;
using AutoMapper;
using Bananagrams.Service.Dtos.DailyWords;
using Bananagrams.Service.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bananagrams.Service.HttpClients;

public interface ITropicalFruitApiService
{
    Task<List<WordDto>> GetAll(string searchWord);
    Task<WordDto> Get(string title);
}

public class TropicalFruitApiService : ITropicalFruitApiService
{
    private static readonly HttpClient _client;
    private readonly IMapper _mapper;

    static TropicalFruitApiService()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("http://tropicalfruitandveg.com")
        };
    }
    
    public TropicalFruitApiService(IMapper mapper) =>
    (_mapper) = (mapper);

    public async Task<List<WordDto>> GetAll(string searchWord)
    {
        var url = string.Format("/api/tfvjsonapi.php?search={0}", searchWord);
        var result = new JsonResponseDto();
        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
        
            result = JsonConvert.DeserializeObject<JsonResponseDto>(stringResponse);
        }
        else
        {
            throw new NotFoundException("No items can be found.");
        }
        
        return _mapper.Map<List<WordDto>>(result.Results);
    }

    public async Task<WordDto> Get(string title)
    {
        var url = string.Format("/api/tfvjsonapi.php?tfvitem={0}", Uri.EscapeUriString(title));
        var result = new JsonResponseDto();
        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            result = JsonConvert.DeserializeObject<JsonResponseDto>(stringResponse);
        }
        
        else
        {
            throw new NotFoundException("No item can be found.");
        }

        return _mapper.Map<WordDto>(result.Results.FirstOrDefault());
    }

}