using System.Security.Claims;

public interface IPointService
{
    Task<List<PointDTO>> GetPoints(PointCoordinateSpanDTO pointSpan);
    Task UpdateBatch(List<PointDTO> points, DomainUser owner);
}