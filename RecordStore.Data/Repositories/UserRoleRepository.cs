using System;
using Microsoft.Data.SqlClient;
using RecordStore.Core.Interfaces;

namespace RecordStore.Data.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly string _connectionString;

        public UserRoleRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public int? GetRoleIdByUserId(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand(
                "SELECT RoleID FROM UserRoles WHERE UserID = @UserID", connection);

            command.Parameters.AddWithValue("@UserID", userId);

            var result = command.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : (int?)null;
        }
    }
}
