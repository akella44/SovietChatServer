﻿using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.RepositoriesImpl;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<User> GetUserByTag(string tag)
    {
        return await _appDbContext.Users
            .FirstAsync(e => e.Tag == tag);
    }
    public async Task<User> GetUserByEmail(string email)
    {
        return await _appDbContext.Users
            .FirstAsync(e => e.Email == email);
    }
    public async Task UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
    public async Task AddUser(User user)
    {
        await _appDbContext.Users.AddAsync(user);
        await _appDbContext.SaveChangesAsync();
    }
    public async Task DeleteUser(User user)
    {
        throw new NotImplementedException();
    }
}