namespace Dottor.Umarell.Shared
{
    public class ApiResult
    {
        public bool Result { get; set; }
    }

    public class ApiResult<T>
    {
        public bool Result { get; set; }
        public T Data { get; set; }
    }
}
