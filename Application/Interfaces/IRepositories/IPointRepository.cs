public interface IPointRepository
{
    Task Update(PointDTO points, DomainUser owner);
    Task<List<Point>> GetPoints();
}