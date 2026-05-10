using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;
using System.Reflection.PortableExecutable;

namespace Siemens.Internship2026.GradeBook.Services;

public class ItemService : IItemService
{
    private readonly IItemReader _reader;

    public ItemService(IItemReader reader)
    {
        _reader = reader; 
    }

    public async Task<IEnumerable<Item>> FirstNItems(int n)
    {
        var items = await _reader.GetAllAsync();

        return items.Where(i => i.Value >= 5 && i.IsActive).Take(n);
    }
}