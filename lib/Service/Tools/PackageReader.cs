using System.Text;
using Service.Core;

namespace Service.Tools;
public class PackageReader{
  public static PackageReader BeginRead(byte[] stream){
    return new PackageReader(stream);
  }

  public PackageReader(byte[] stream){
    ByteStream = stream;
    ushort functionNameSize = BitConverter.ToUInt16(stream, sizeof(ushort));
    ushort serviceNameSize = BitConverter.ToUInt16(stream, 0);
    PayloadStart = sizeof(ushort)* 2 + functionNameSize + serviceNameSize;
    currentIndex = PayloadStart;
  }

  public BitTable Peek(){
    return (BitTable) ByteStream[currentIndex];
  }

  public bool CanRead(){
    if(ByteStream[currentIndex] == (byte) BitTable.EOF){
      int indexBuffer = currentIndex;
      for (int i = 0; i < 3; i++)
      {
        if(ByteStream[indexBuffer++] != (byte)BitTable.EOF)
          return true;
      }
      return false;
    }
    return true;
  }


  public string String(){

    if(!ValidRequest(BitTable.STRING)){
      throw  new InvalidCastException("Could not cast to string");
    }
    currentIndex++;

    int stringSize = BitConverter.ToInt32(ByteStream, currentIndex);
    currentIndex += sizeof(Int32);
    string result = Encoding.Unicode.GetString(ByteStream, currentIndex, stringSize);
    currentIndex += stringSize;
    return result;
  }


  public int Int(){
    if(!ValidRequest(BitTable.INT)){
      throw  new InvalidCastException("could not cast to int");
    }

    currentIndex++;
    int result = BitConverter.ToInt32(ByteStream, currentIndex);
    currentIndex += sizeof(Int32);

    return result;
  }





  bool ValidRequest(BitTable targetType){
    return ByteStream[currentIndex] == (byte) targetType;
  }


  int PayloadStart = 0;
  int currentIndex = 0;
  byte[] PackageHeader {get; set;}
  byte[] ByteStream {get; set;}

}