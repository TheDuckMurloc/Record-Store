using RecordStore.Core.Models;

namespace RecordStore.Core.Interfaces;

public interface IRecordRepository
{
    IEnumerable<Record> GetAll();
    IEnumerable<Record> GetAllRecords();
    Record? GetById(int id);
    Record? GetRecordById(int id);
    void DecreaseStock(int recordId, int quantity);

}

