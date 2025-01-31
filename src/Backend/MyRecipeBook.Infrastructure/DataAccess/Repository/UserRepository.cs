﻿using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.Infrastructure.DataAccess.Repository
{
    internal class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
    {
        private readonly MyRecipeBookDbContext _dbContext;

        public UserRepository(MyRecipeBookDbContext dbContext) => _dbContext = dbContext;
        
        public async Task Add(User user) => await _dbContext.AddAsync(user);

        public async Task<bool> ExistActiveUserWithEmail(string email)
        {
           return await _dbContext.Users.AnyAsync(u => u.Email.Equals(email) && u.Active);
        }
        
    }
}
