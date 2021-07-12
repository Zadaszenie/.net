using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisacomWepApp.Models;

namespace VisacomWepApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly WebAppContext _context;
        public UserRepository(WebAppContext context)
        {
            _context = context;
        }
        public User Create(User user)
        {
            _context.Users.Add(user);

            try
            {
                _context.SaveChanges();
            }
            catch(Exception err) 
            {
                
            }

            return user;

        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

    }
}
