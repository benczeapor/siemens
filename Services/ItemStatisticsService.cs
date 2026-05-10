using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

public class ItemStatisticsService : IItemStatisticsService
{
    public ItemSummaryDTO CalculateStats(IEnumerable<Item> items)
    {
        var itemList = items.ToList();

        var totalCount = itemList.Count;
        var averageValue = itemList.Any() ? itemList.Average(i => i.Value) : 0;

        return new ItemSummaryDTO(totalCount, averageValue, DateTime.UtcNow);
    }
}