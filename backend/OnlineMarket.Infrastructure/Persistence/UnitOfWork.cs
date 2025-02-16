using OnlineMarket.Application.Interfaces;
using OnlineMarket.Infrastructure.Data;

namespace OnlineMarket.Infrastructure.Persistence;

public class UnitOfWork(OnlineShopDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        await dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await dbContext.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await dbContext.Database.RollbackTransactionAsync();
    }
}