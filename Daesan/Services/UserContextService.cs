using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Daesan.Models.Data;

namespace Daesan.Services
{
    public class UserContextService
    {
        private readonly DatabaseContext _database;

        public UserContextService(DatabaseContext database)
        {
            _database = database;
        }

        public async Task<User> GetUserAsync(string id)
        {

            var user = await _database.Users.FindAsync(id);

            if (user == null)
            {
                user = new User(id);

                _database.Users.Add(user);
                await _database.SaveChangesAsync();
            }

            return user;
        }
    }
}
