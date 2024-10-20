namespace Service.Models;
public class RequestModel{
    public int Id { get; set; }
    public string ServiceName { get; set; }
    public string FunctionName { get; set; }
    public byte[] Payload { get; set; }
}