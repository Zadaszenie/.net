using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisacomWepApp.Models;

namespace VisacomWepApp.Data
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetByEmail(string email);

        Task<User> GetByEmailAsync(string email);

        User GetById(int ID);
    }
}
