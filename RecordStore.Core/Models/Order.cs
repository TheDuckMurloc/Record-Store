using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordStore.Core
{
    internal class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateOnly OrderDate { get; set; }
        public int total { get; set; }
    }
}
