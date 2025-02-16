namespace OnlineMarket.Application.Interfaces;

public interface IUnitOfWork
{
    public Task SaveChangesAsync();
    public Task BeginTransactionAsync();
    public Task CommitTransactionAsync();
    public Task RollbackTransactionAsync();
}