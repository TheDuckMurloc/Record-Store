using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using RecordStore.Core.Interfaces;
using RecordStore.Core.Models;

namespace RecordStore.Data.Repositories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly string _connectionString;

        public RecordRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IEnumerable<Record> GetAll()
        {
            return GetAllRecords();
        }

        public IEnumerable<Record> GetAllRecords()
        {
            var records = new List<Record>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand(
                "SELECT r.RecordID, r.Title, r.Price, r.Stock, r.CoverImageURL, " +
                "a.ArtistID, a.Name AS ArtistName, " +
                "g.GenreID, g.Name AS GenreName " +
                "FROM Records r " +
                "JOIN Artists a ON r.ArtistID = a.ArtistID " +
                "JOIN Genres g ON r.GenreID = g.GenreID", connection);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var record = new Record
                {
                    RecordID = reader.GetInt32(reader.GetOrdinal("RecordID")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                    Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                    CoverImageUrl = reader.GetString(reader.GetOrdinal("CoverImageURL")),
                    Artist = new Artist
                    {
                        ArtistID = reader.GetInt32(reader.GetOrdinal("ArtistID")),
                        Name = reader.GetString(reader.GetOrdinal("ArtistName"))
                    },
                    Genre = new Genre
                    {
                        GenreID = reader.GetInt32(reader.GetOrdinal("GenreID")),
                        Name = reader.GetString(reader.GetOrdinal("GenreName"))
                    }
                };
                records.Add(record);
            }

            return records;
        }

        public Record? GetById(int id)
        {
            return GetRecordById(id);
        }

        public Record? GetRecordById(int id)
        {
            Record? record = null;

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand(
                "SELECT r.RecordID, r.Title, r.Price, r.Stock, r.CoverImageURL, " +
                "a.ArtistID, a.Name AS ArtistName, " +
                "g.GenreID, g.Name AS GenreName " +
                "FROM Records r " +
                "JOIN Artists a ON r.ArtistID = a.ArtistID " +
                "JOIN Genres g ON r.GenreID = g.GenreID " +
                "WHERE r.RecordID = @RecordID", connection);

            command.Parameters.AddWithValue("@RecordID", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                record = new Record
                {
                    RecordID = reader.GetInt32(reader.GetOrdinal("RecordID")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                    Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                    CoverImageUrl = reader.GetString(reader.GetOrdinal("CoverImageURL")),
                    Artist = new Artist
                    {
                        ArtistID = reader.GetInt32(reader.GetOrdinal("ArtistID")),
                        Name = reader.GetString(reader.GetOrdinal("ArtistName"))
                    },
                    Genre = new Genre
                    {
                        GenreID = reader.GetInt32(reader.GetOrdinal("GenreID")),
                        Name = reader.GetString(reader.GetOrdinal("GenreName"))
                    }
                };
            }

            return record;
        }
    }
}
