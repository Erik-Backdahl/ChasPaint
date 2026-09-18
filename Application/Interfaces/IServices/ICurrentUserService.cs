public interface ICurrentUserService
{
    Task<DomainUser?> GetCurrentUserAsync();
}