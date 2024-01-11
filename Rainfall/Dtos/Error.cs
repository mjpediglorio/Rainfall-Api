namespace Rainfall.Dtos
{
    public class Error
    {
        public string Message { get; set; }
        public ErrorDetail[] Detail { get; set; }
    }
}
