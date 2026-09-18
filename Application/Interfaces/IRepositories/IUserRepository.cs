public interface IUserRepository
{
    Task AddAsync(DomainUser domainUser);
    Task RemoveAsync(DomainUser domainUser);
    Task<DomainUser?> GetByIdAsync(Guid id);
    Task<DomainUser?> GetByApplicationUserIdAsync(string applicationUserId);
}