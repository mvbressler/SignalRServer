namespace GSIMSignalRServerProject.Domain.Models
{
    public class LenelObjectsRequest
    {
        public string? Server { get; set; }
        public string? TypeName { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        
    }
}
