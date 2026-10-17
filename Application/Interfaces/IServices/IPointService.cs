using System.Security.Claims;

public interface IPointService
{
    Task<List<PointDTO>> GetPoints(PointCoordinateSpanDTO pointSpan);
    Task Update(PointDTO point, DomainUser owner);
}