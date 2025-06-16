using RecordStore.Core.Interfaces;
using RecordStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecordStore.Data.Services
{

    public class RecordService
    {
        private readonly IRecordRepository _recordRepository;

        public RecordService(IRecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public List<Record> GetRecords(string? searchTerm = null)
        {
            var records = _recordRepository.GetAllRecords();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                records = records.Where(r =>
                    r.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    r.Artist.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                );
            }

            return records.ToList();
        }

        public Record? GetRecordById(int id) => _recordRepository.GetRecordById(id);
    }
}