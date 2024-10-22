namespace Service.Models;
public class RequestModel{
  public string FunctionClientName { get; set; }
  public string ServiceClientName { get; set; }

  public byte[] ConverToByte(){
    using MemoryStream s = new MemoryStream();
    using BinaryWriter w = new BinaryWriter(s);

    w.Write(ServiceClientName);
    w.Write(FunctionClientName);

    return s.GetBuffer();
  }




}