using BackEnd.Data.Context;

namespace BackEnd.Data.Models;

public abstract class DataLoader
{
    public abstract Task LoadDataAsync(Stream fileStream, ConferencePlannerContext db);
}
