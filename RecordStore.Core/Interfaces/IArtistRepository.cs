using RecordStore.Core;
using RecordStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordStore.Core.Interfaces
{
    internal interface IArtistRepository
    {
        IEnumerable<Artist> GetAll();
        Artist? GetById(int id);
        void Add(Artist artist);
        void Update(Artist artist);
        void Delete(int id);
    }
}