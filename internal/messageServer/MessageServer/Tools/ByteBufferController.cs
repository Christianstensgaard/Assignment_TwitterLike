using System.Drawing;
using System.Net.Sockets;

namespace MessageServer.Tools;
public class ByteBufferController{
  const int BufferSize = 10000;
  const int bufffermargin = 500;

  



  public int Copy(byte[] stream){
    return -1;
  }

  public ByteArray ReadNetworkStream(NetworkStream stream){
    int totalBytesRead = 0;
    int readSize = 256;
    int bytesRead = 0;

     while (stream.DataAvailable){
      bytesRead = stream.Read(globalBuffer, currentBufferPosition + totalBytesRead, readSize);

        if (bytesRead == 0)
          break;

        totalBytesRead += bytesRead;

        if (currentBufferPosition + totalBytesRead >= globalBuffer.Length)
        {
          throw new InvalidOperationException("Buffer too small to hold the incoming data.");
        }
     }

    return new ByteArray(globalBuffer, ScalePosition(totalBytesRead), currentBufferPosition + totalBytesRead);
  }

  public void Free(ByteArray array){
    //- Not Needed atm...
  }

  public ByteArray Allocate(byte[] stream){
    int size = stream.Length;
    Check(size);
    int start = ScalePosition(size);
    Array.Copy(stream,0,globalBuffer, start, size);
    return new ByteArray(globalBuffer, start, start+size);
  }

  public ByteArray Allocate(int size){
    Check(size);
    return new ByteArray(globalBuffer, ScalePosition(size), currentBufferPosition + size);
  }

  int ScalePosition(int size){
    int current = currentBufferPosition;
    currentBufferPosition += size;
    return current;
  }

  void ClearBuffer(){
    currentBufferPosition = 0;
  }

  void Check(int size){
    if(currentBufferPosition + size >= BufferSize - bufffermargin ){
      ClearBuffer();
    }
  }

  int currentBufferPosition = 0;
  byte[] globalBuffer = new byte[BufferSize];
}

public class ByteArray{
  public ByteArray(byte[] globalSource, int start, int end){
    pBuffer = globalSource;
    Start = start;
    End = end;
    Length = End - Start;
    Stream = pBuffer;
  }
  public int Start { get;  private set; }
  int End { get;  set; }
  public int Length { get; private set; }
  public byte[] Stream {get; private set;}

  public byte this[int index]
  {
    get
    {
      //- Missing OOB
      return pBuffer[Start+index];
    }
    set
    {
      //- Missing OOB
      pBuffer[Start+index] = value;
    }
  }
  byte[] pBuffer;
}