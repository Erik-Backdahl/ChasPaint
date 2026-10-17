using System.Net.NetworkInformation;
using System.Security.AccessControl;

public class PointService : IPointService
{
    private readonly IPointRepository _pointRepository;
    public PointService(IPointRepository pointRepository)
    {
        _pointRepository = pointRepository;
    }
    public async Task<List<PointDTO>> GetPoints()
    {
        var rawPoints = await _pointRepository.GetPoints();

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
    public async Task Update(PointDTO point, DomainUser owner)
    {
        await _pointRepository.Update(point, owner);
    }
}