using Final.Models;

namespace Final.Services;

public interface IStockService
{
    List<Stock> GetAll();
    List<Stock> GetAll(string nameFiler);
    void Update(Stock obj);
    void Delete(Stock obj);
    Stock? GetById(int id);
    Stock? GetStockByArtistIdAndAlbumId(int PersonId, int ClotheId);
}