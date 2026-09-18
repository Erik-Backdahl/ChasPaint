public interface IUserRepository
{
    Task AddAsync(DomainUser domainUser);
    Task RemoveAsync(DomainUser domainUser);
}