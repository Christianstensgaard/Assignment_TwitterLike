using System.Runtime.InteropServices;
using System.Text;

namespace Service.Tools;
public class ServiceStreamReader : IDisposable
{
  public static ServiceStreamReader Create(byte[] Stream) => new ServiceStreamReader(Stream);

  public ServiceStreamReader(byte[] stream){
    pStream = stream;
  }

  public void Peek(){

  }

  public void Reset(){

  }


  public string? String(){
    if(pStream[index] != ID_STRING){
      return null;
    }

    index++;
    int stringSize = BitConverter.ToInt32(pStream, appendIndexSize(sizeof(Int32)));
    string value = Encoding.Unicode.GetString(pStream, index, appendIndexSize(stringSize));
    return value;
  }

  public int? Int(){
    if(pStream[index] != ID_INT){
      return null;
    }
    index++;
    return BitConverter.ToInt32(pStream, appendIndexSize(sizeof(Int32)));
  }


  public void Dispose()
  {
  }


  const byte ID_STRING = 0xEE;
  const byte ID_INT = 0xFF;


  int appendIndexSize(int size){
    int current = index;
    index += size;
    return current;
  }


  int index = 0;

  byte[] pStream;
}