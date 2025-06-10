using RecordStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordStore.Core.Models;

namespace RecordStore.Core.Interfaces
{
    public interface ICartRepository
    {
        List<CartItem> LoadCart();
        void SaveCart(List<CartItem> cart);
        void ClearCart();
    }
}
