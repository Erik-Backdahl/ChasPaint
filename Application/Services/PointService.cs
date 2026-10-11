using System.Net.NetworkInformation;
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
        if (pointSpan.MaxX > 500)
            throw new BadRequestException("Max X coordinate is 500");
        if (pointSpan.MinX < 0)
            throw new BadRequestException("Minimum X coordinate is 0");
        if (pointSpan.MaxY > 500)
            throw new BadRequestException("Max Y coordinate is 500");
        if (pointSpan.MinY < 0)
            throw new BadRequestException("Min Y coordinate is 0");


        var rawPoints = await _pointRepository.GetPoints(pointSpan);

        return rawPoints.Select(point => new PointDTO
        {
            YCoordinate = point.YCoordinate,
            XCoordinate = point.XCoordinate,
            ColorHex = point.ColorHex,
            /* point.Owner is null ? null : new UserDTO
            {
                UserName = point.Owner.UserName
            } */
        }).ToList();
    }
    public async Task UpdateBatch(List<PointDTO> points, DomainUser owner)
    {
        foreach (PointDTO point in points)
        {
            if (point.YCoordinate < 0 || point.YCoordinate > 500)
                throw new Exception("Y coordinate out of span");
            if (point.XCoordinate < 0 || point.XCoordinate > 500)
                throw new Exception("X coordinate out of span");
        }
        await _pointRepository.UpdateBatch(points, owner);
    }
}