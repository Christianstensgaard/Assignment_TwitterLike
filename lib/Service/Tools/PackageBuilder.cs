using System.Text;
using Service.Core;

namespace Service.Tools;
public class PackageBuilder{
  public PackageBuilder(byte[] header){
    ByteStream = new List<byte>();
    ByteStream.AddRange(header);
  }

  public void PackHeader(byte[] header){
    ByteStream.AddRange(header);
  }
  public void Pack(params int[] ints){
    for (int i = 0; i < ints.Length; i++)
    {
      ByteStream.Add((byte)BitTable.INT);
      ByteStream.AddRange(BitConverter.GetBytes(ints[i]));
    }
  }
  public void Pack(params string[] strings){
    for (int i = 0; i < strings.Length; i++)
    {
      ByteStream.Add((byte) BitTable.STRING);
      byte[] stringbuffer = Encoding.Unicode.GetBytes(strings[i]);
      int stringSize = stringbuffer.Length;
      ByteStream.AddRange(BitConverter.GetBytes(stringSize));
      ByteStream.AddRange(stringbuffer);
    }
  }
  public byte[] PackAndSign(){

    ByteStream.AddRange([(byte)BitTable.EOF, (byte)BitTable.EOF, (byte)BitTable.EOF]);
    return [.. ByteStream];
  }

  //- More is maybe needed
  public List<byte> ByteStream {get; private set;}
}