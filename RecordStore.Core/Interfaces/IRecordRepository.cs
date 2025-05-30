using RecordStore.Core.Models;

namespace RecordStore.Core.Interfaces;

public interface IRecordRepository
{
    IEnumerable<Record> GetAll();
    Record? GetById(int id);
    void Add(Record record);
    void Update(Record record);
    void Delete(int id);
}
