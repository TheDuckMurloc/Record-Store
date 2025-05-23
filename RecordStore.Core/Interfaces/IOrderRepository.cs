﻿using RecordStore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordStore.Business.Interfaces
{
    internal interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order? GetById(int id);
        void Add(Order order);
        void Update(Order order);
        void Delete(int id);
    }
}