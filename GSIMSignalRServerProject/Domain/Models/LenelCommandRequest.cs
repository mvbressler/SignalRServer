namespace GSIMSignalRServerProject.Domain.Models
{
    public class LenelCommandRequest
    {
        public string MethodName { get; set; }
        public string TypeName { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public Dictionary<string, object> InParams { get; set; }
    }
}
