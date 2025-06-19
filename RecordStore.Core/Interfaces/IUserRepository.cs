using RecordStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordStore.Core.Interfaces
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);   
    }

}
