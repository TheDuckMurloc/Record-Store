using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordStore.Core.Interfaces;
using RecordStore.Core.Models;

namespace RecordStore.Core.Services
{
    public class AdminService : IAdminRepository
    {
        private readonly IRecordRepository _recordRepository;

        public AdminService(IRecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public void UpdateStock(int recordId, int newStock)
        {
            _recordRepository.UpdateStock(recordId, newStock);
        }
    }
}
