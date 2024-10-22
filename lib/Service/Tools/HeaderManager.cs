using System.Drawing;
using System.Text;

namespace Service.Tools;
public class HeaderManager{

  const int ServiceClientNameSize = 55;
  const int ServiceFunctionNameSize = 55;

  public static byte[] CreateHeader(string ServiceClientName, string ServiceFunctionName){
    byte[] RootName = Encoding.Unicode.GetBytes(ServiceClientName);
    byte[] functionName = Encoding.Unicode.GetBytes(ServiceFunctionName);
    byte[] resultBuffer = new byte[RootName.Length + functionName.Length + ( 2 * sizeof(ushort) )];

    Array.Copy(BitConverter.GetBytes(RootName.Length), resultBuffer, sizeof(ushort));
    Array.Copy(BitConverter.GetBytes(functionName.Length),0, resultBuffer, sizeof(ushort), sizeof(ushort));
    Array.Copy(RootName, 0, resultBuffer, sizeof(ushort) * 2, RootName.Length);
    Array.Copy(functionName, 0, resultBuffer, (sizeof(ushort) * 2) + RootName.Length , functionName.Length);

    return resultBuffer;
  }
  




  public static bool EqualServiceName(byte[] t1,  byte[] t2){
    ushort targetOneSize = BitConverter.ToUInt16(t1, 0);
    ushort targetTwoSize = BitConverter.ToUInt16(t2, 0);
    if(targetOneSize != targetTwoSize)
      return false;

    bool result = true;
    for (int i = sizeof(ushort) * 2; i < targetOneSize; i++)
    {
      if(t1[i] != t2[i]){
        result = false;
        break;
      }
    }
    return result;
  }
  public static bool EqualFunctionName(byte[] t1, byte[] t2){
    int startIndex = BitConverter.ToInt16(t1,0);
    ushort targetOneSize = BitConverter.ToUInt16(t1, sizeof(ushort));
    ushort targetTwoSize = BitConverter.ToUInt16(t2, sizeof(ushort));

    if(targetOneSize != targetTwoSize)
      return false;

    bool result = true;
    for (int i = sizeof(ushort) * 2 + startIndex; i < targetOneSize + (sizeof(ushort) *2) + startIndex; i++)
    {
      if(t1[i] != t2[i]){
        result = false;
        break;
      }
    }
    return result;
  }
}