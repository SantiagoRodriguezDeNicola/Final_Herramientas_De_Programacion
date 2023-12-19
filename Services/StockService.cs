using Final.Data;
using Final.Models;

namespace Final.Services;

public class StockService : IStockService
{
    private readonly ClotheContext _context;
    
    public StockService(ClotheContext context){
        _context = context;
    }
    public void Delete(Stock obj)
    {
        _context.Stock.Remove(obj);
        _context.SaveChanges();
    }

    public List<Stock> GetAll()
    {
        List<Stock> stocks = new List<Stock>();
        stocks = _context.Stock.ToList();
        return stocks;
    }

    public List<Stock> GetAll(string nameFiler)
    {
        throw new NotImplementedException();
    }

    public Stock? GetById(int id)
    {
        return _context.Stock.FirstOrDefault(m => m.Id == id);
    }

    public Stock? GetStockByArtistIdAndAlbumId(int artistId, int albumId)
    {
        var stock = _context.Stock.Where(i => i.BrandId == artistId && i.ClotheId == albumId).FirstOrDefault();
        return stock;
    }

    public void Update(Stock obj)
    {
        _context.Update(obj);
        _context.SaveChanges();
    }
}
