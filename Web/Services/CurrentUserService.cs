using Microsoft.AspNetCore.Identity;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserRepository _userRepository;

    public CurrentUserService(
        IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager,
        IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<DomainUser?> GetCurrentUserAsync()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user is null) return null;

        var userId = _userManager.GetUserId(user);
        if (userId is null) return null;

        return await _userRepository.GetByApplicationUserIdAsync(userId);
    }
}