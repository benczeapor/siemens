using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;
using System.Net.Http.Json;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class ItemRepository : IItemReader
{
    protected readonly List<Item> _items = new();
    protected int _nextId = 1;

    private readonly HttpClient _httpClient;
    private readonly ILogger<ItemRepository> _logger;

    private const string DataUrl = "ArdeleanTudor/8ea407832cd9794960e0e6bbd1319f6e/raw/145";

    public ItemRepository(HttpClient httpClient, ILogger<ItemRepository> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public virtual async Task<IEnumerable<Item>> GetAllAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(DataUrl);

            if(!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("External API returned {StatusCode} for items.", response.StatusCode);
                return Enumerable.Empty<Item>();
            }
            var items = await response.Content.ReadFromJsonAsync<List<Item>>();
            return items?.Where(i => i.IsActive) ?? Enumerable.Empty<Item>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Network error occurred while fetching items.");
            return Enumerable.Empty<Item>();
        }
    }

    public virtual async Task<Item?> GetByIdAsync(int id)
    {
        var items = await GetAllAsync();
        return items.FirstOrDefault(i => i.Id == id);
    }
}
