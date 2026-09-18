using Microsoft.AspNetCore.Identity;
public class RegisterUserHandler
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly ChasPaintDbContext _dbContext;

    public RegisterUserHandler(
        UserManager<ApplicationUser> userManager,
        IUserRepository userRepository,
        ChasPaintDbContext dbContext)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _dbContext = dbContext;
    }

    public async Task<IdentityResult> Handle(RegisterUserCommand cmd)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        var domainUser = new DomainUser
        {
            Id = Guid.NewGuid(),
            UserName = cmd.UserName,
        };
        await _userRepository.AddAsync(domainUser);

        var appUser = new ApplicationUser
        {
            UserName = cmd.Email,
            Email = cmd.Email,
            DomainUserId = domainUser.Id
        };

        var identityResult = await _userManager.CreateAsync(appUser, cmd.Password);

        if (!identityResult.Succeeded)
        {
            await transaction.RollbackAsync();
            return identityResult;
        }

        await transaction.CommitAsync();
        return identityResult;
    }
}