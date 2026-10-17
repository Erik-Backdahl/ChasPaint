using System.Security.Claims;

public interface IPointService
{
    Task<List<PointDTO>> GetPoints();
    Task Update(PointDTO point, DomainUser owner);
}