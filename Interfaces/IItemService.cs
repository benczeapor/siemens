using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Interfaces;

public interface IItemService
{
    Task<IEnumerable<Item>> FirstNItems(int n);
}