using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Interfaces;

public interface IItemStatisticsService
{
    ItemSummaryDTO CalculateStats(IEnumerable<Item> items);
}