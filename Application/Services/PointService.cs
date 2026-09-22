using System.Security.AccessControl;

public class PointService : IPointService
{
    private readonly IPointRepository _pointRepository;
    public PointService(IPointRepository pointRepository)
    {
        _pointRepository = pointRepository;
    }
    public async Task<List<PointDTO>> GetPoints(PointCoordinateSpanDTO pointSpan)
    {
        //skriver limiter här.

        var rawPoints = await _pointRepository.GetPoints(pointSpan);

        return rawPoints.Select(point => new PointDTO
        {
            YCoordinate = point.YCoordinate,
            XCoordinate = point.XCoordinate,
            ColorHex = point.ColorHex,
            Owner = point.Owner is null ? null : new UserDTO
            {
                UserName = point.Owner.UserName
            }
        }).ToList();
    }
    public async Task UpdateBatch(List<PointDTO> points, DomainUser owner)
    {
        await _pointRepository.UpdateBatch(points, owner);
    }
}