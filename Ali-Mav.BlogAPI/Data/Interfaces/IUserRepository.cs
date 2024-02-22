﻿using Ali_Mav.BlogAPI.Models;

namespace Ali_Mav.BlogAPI.Data.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task AddUsers(List<User> users);
        Task<List<User>> SearchAsync(string key);
    }
}
