public interface IPointRepository
{
    Task UpdateBatch(List<PointDTO> points, DomainUser owner);
    Task<List<Point>> GetPoints(PointCoordinateSpanDTO pointSpan);
}