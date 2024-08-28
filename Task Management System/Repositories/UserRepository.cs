using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task_Management_System.Core.ModelEntity;
using Task_Management_System.Context;
using Task_Management_System.Shared;

namespace Task_Management_System.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _ApplicationDbContext;
        public UserRepository(ApplicationDbContext ApplicationDbContext) : base(ApplicationDbContext)
        {
            _ApplicationDbContext = ApplicationDbContext;
        }

        public async Task<User?> GetUserByUsernameAndPassword(string username, string password)
        {
            return await _ApplicationDbContext.Users.SingleOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() && x.PasswordUser == password);
        }
    }
}
