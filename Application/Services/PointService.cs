public class PointService : IPointService
{
    private readonly IPointRepository _pointRepository;
    public PointService(IPointRepository pointRepository)
    {
        _pointRepository = pointRepository;
    }
    public async Task UpdateBatch(List<PointDTO> points, DomainUser owner)
    {
        await _pointRepository.UpdateBatch(points, owner);
    }
}