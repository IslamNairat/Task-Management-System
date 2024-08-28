namespace Task_Management_System.Shared.Pagination
{
    public class PaginationDto<T>
    {
        public List<T> Result { get; set; }
        public int Total { get; set; }
    }
}
