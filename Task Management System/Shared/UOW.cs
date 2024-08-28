using Microsoft.EntityFrameworkCore;
using Task_Management_System.Context;
using Task_Management_System.Shared.Interface;

namespace Task_Management_System.Shared
{
    public class UOW<TContext> : IUOW<TContext> where TContext : DbContext
    {
        public ApplicationDbContext _context;

        public UOW(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
