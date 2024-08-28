using Microsoft.EntityFrameworkCore;

namespace Task_Management_System.Shared.Interface
{
    public interface IUOW<T> where T : DbContext, IDisposable
    {
        Task CommitAsync();
    }
}
