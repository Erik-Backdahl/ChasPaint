using System.Security.Claims;

public interface IPointService
{
    Task UpdateBatch(List<PointDTO> points, DomainUser owner);
}