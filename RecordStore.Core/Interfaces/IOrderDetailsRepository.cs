using RecordStore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordStore.Business.Interfaces
{
    internal interface IOrderDetailsRepository
    {
        IEnumerable<OrderDetails> GetAll();
        OrderDetails? GetById(int id);
        void Add(OrderDetails orderdetails);
        void Update(OrderDetails orderdetails);
        void Delete(int id);
    }
}