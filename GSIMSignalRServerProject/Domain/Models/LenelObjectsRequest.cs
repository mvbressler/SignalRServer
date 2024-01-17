namespace GSIMSignalRServerProject.Domain.Models
{
    public class LenelObjectsRequest
    {
        public string? TypeName { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Server { get; set; }
    }
}
