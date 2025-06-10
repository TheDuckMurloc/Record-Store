using RecordStore.Core;
using RecordStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordStore.Business.Interfaces
{
    internal interface IGenreRepository
    {
        IEnumerable<Genre> GetAll();
        Genre GetById(int id);
        void Add(Genre record);
        void Update(Record record);
        void Delete(int id);
    }
}