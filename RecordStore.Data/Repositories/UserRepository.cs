using System;
using Microsoft.Data.SqlClient;
using RecordStore.Core.Interfaces;
using RecordStore.Core.Models;

namespace RecordStore.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public User? GetByUsername(string username)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand(
                "SELECT UserID, Username, PasswordHash FROM Users WHERE Username = @Username", connection);

            command.Parameters.AddWithValue("@Username", username);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                    Username = reader.GetString(reader.GetOrdinal("Username")),
                    PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                };
            }

            return null;
        }
        
    }
}
