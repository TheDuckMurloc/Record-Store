using RecordStore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordStore.Business.Interfaces
{
    internal interface IRecordRepository
    {
        IEnumerable<Record> GetAll();
        Record? GetById(int id);
        void Add(Record record);
        void Update(Record record);
        void Delete(int id);
    }
}
