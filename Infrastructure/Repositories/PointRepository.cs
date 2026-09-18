using Microsoft.EntityFrameworkCore;

public class PointRepository : IPointRepository
{
    private readonly ChasPaintDbContext _dbContext;
    public PointRepository(ChasPaintDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Point>> GetPoints(int xMin, int xMax, int yMin, int yMax)
    {
        return await _dbContext.Points
            .Where(
                p => p.XCoordinate >= xMin && p.XCoordinate <= xMax
                && p.YCoordinate >= yMin && p.YCoordinate <= yMax)
            .ToListAsync();
    }

    public async Task UpdateBatch(List<PointDTO> newPoints, DomainUser owner)
    {
        foreach (var point in newPoints)
        {
            var existing = await _dbContext.Points.FindAsync(point.XCoordinate, point.YCoordinate);
            
            if (existing != null)
            {
                existing.ColorHex = point.ColorHex;
                existing.Owner = owner;
            }
            else
            {
                _dbContext.Points.Add(new Point
                {
                    XCoordinate = point.XCoordinate,
                    YCoordinate = point.YCoordinate,
                    ColorHex = point.ColorHex,
                    Owner = owner
                });
            }
        }
        await _dbContext.SaveChangesAsync();
    }
}