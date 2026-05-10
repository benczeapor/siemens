using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemReader _reader;
    private readonly IItemStatisticsService _itemStatisticsService;
    private readonly ILogger<ItemController> _logger;

    public ItemController(IItemReader reader, IItemStatisticsService itemStatisticsService, ILogger<ItemController> logger)
    {
        _reader = reader;
        _itemStatisticsService = itemStatisticsService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        //Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/item called");
        _logger.LogInformation($"[LOG] {DateTime.UtcNow}: GET api/item called");

        var items = await _reader.GetAllAsync();
        var statistics = _itemStatisticsService.CalculateStats( items );

        //Console.WriteLine($"[LOG] Returning {totalCount} items, average value: {averageValue}");

        return Ok(new
        {
            Data = items,
            Statistics = statistics
        });
    }

    [HttpGet("{id:int:min(1)}")]
    public async Task<IActionResult> GetById(int id)
    {
        //Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/item/{id} called");

        _logger.LogInformation($"[LOG] {DateTime.UtcNow}: GET api/item/{id} called");

        var item = await _reader.GetByIdAsync(id);

        if (item == null)
        {
            //Console.WriteLine($"[LOG] Item {id} not found");
            _logger.LogWarning($"[LOG] Item {id} not found");
            return NotFound($"Item with Id {id} was not found.");
        }

        return Ok(item);
    }
}
