namespace Task_Management_System.Shared.Helper
{
    public static class Helpers
    {
        public static IQueryable<T> Pagination<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }
}
