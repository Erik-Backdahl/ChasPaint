using Microsoft.EntityFrameworkCore;

public class PointRepository : IPointRepository
{
    private readonly ChasPaintDbContext _dbContext;
    public PointRepository(ChasPaintDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Point>> GetPoints()
    {
        int serverMinY = 0;
        int serverMaxY = 249;
        int serverMinX = 0;
        int serverMaxX = 249;


        return await _dbContext.Points
            .Where(p => p.XCoordinate >= serverMinX && p.XCoordinate <= serverMaxX
            && p.YCoordinate >= serverMinY && p.YCoordinate <= serverMaxY)
            .Include(p => p.Owner)
            .ToListAsync();
    }

    public async Task Update(PointDTO point, DomainUser owner)
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

        await _dbContext.SaveChangesAsync();
    }
}