﻿using RecordStore.Core.Interfaces;
using RecordStore.Core.Models;
using System.Security.Cryptography;
using System.Text;

public class UserService
{
    private readonly IUserRepository _repo;
    private readonly IUserRoleRepository _roleRepo;

    public UserService(IUserRepository repo, IUserRoleRepository roleRepo)
    {
        _repo = repo;
        _roleRepo = roleRepo;
    }

    public User? Login(string username, string password)
    {
        var user = _repo.GetByUsername(username);
        if (user == null) return null;

        string hashedInput = ComputeSha256Hash(password);
        if (hashedInput == user.PasswordHash)
        {
            var roleId = _roleRepo.GetRoleIdByUserId(user.UserID);
            if (roleId.HasValue)
            {
                user.Role = roleId.Value.ToString(); 
            }

            return user;
        }

        return null;
    }

    public string ComputeSha256Hash(string rawData)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = Encoding.Unicode.GetBytes(rawData);
        byte[] hash = sha256.ComputeHash(bytes);
        return BitConverter.ToString(hash).Replace("-", "").ToUpperInvariant();
    }
}
