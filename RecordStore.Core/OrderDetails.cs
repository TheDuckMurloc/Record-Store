﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordStore.Core
{
    internal class OrderDetails
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int RecordID { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
