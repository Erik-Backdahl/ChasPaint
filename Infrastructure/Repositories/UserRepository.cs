public class UserRepository : IUserRepository
{
    private readonly ChasPaintDbContext _dbContext;
    public UserRepository(ChasPaintDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(DomainUser domainUser)
    {
        await _dbContext.DomainUsers.AddAsync(domainUser);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(DomainUser domainUser)
    {
        _dbContext.DomainUsers.Remove(domainUser);
        await _dbContext.SaveChangesAsync();
    }
}