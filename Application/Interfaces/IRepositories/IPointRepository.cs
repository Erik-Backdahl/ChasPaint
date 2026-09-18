public interface IPointRepository
{
    Task UpdateBatch(List<PointDTO> points, DomainUser owner);
    Task<List<Point>> GetPoints(int xMin, int xMax, int yMin, int yMax);
}