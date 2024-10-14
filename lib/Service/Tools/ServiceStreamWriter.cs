using System.Text;

namespace Service.Tools;
public class ServiceSteramWriter : IDisposable
{
  public static ServiceSteramWriter Create(byte[] Stream) => new ServiceSteramWriter(Stream);

  public ServiceSteramWriter(byte[] stream){
    pStream = stream;
  }


  public int Index { get; set; } = 0;
  public bool NoError { get; set; } = true;


  public void InsertInt(int value){

  }
  public void InsertString(string value){
      int stringSize = value.Length;
      char[] stringToCharArray = value.ToCharArray();

      System.Console.WriteLine(stringSize);

      pStream[Index++] = ID_STRING;
      





  }
















  public void Dispose()
  {
  }


  const byte ID_STRING = 0xEE;
  const byte ID_INT = 0xFF;




  byte[] pStream;
}