using System.Text;

namespace RabbitMqDefault.Converter;
public class AccountConverter{

  public static AccountConverter BeginConverting(){
    return new AccountConverter();
  }

  public byte[] ToStream(AccountModel model){
    if(!IsChanged && Stream != null)
      return Stream;

    this.model = model;
    string payload = $"{model.Username},{model.Password},{model.Activity}";
    byte[] stringBuffer = Encoding.UTF8.GetBytes(payload);

    int checksum = stringBuffer.Length + 1;

    ushort inputChecksum = (ushort) checksum;
    Stream = new byte[stringBuffer.Length + 1 + sizeof(ushort)];
    Stream[0] = accountIdentity;
    Array.Copy(BitConverter.GetBytes(inputChecksum), 0, Stream, 1, sizeof(ushort));
    Array.Copy(stringBuffer, 0, Stream, 1+ sizeof(ushort), stringBuffer.Length);

    return Stream;
  }

  public AccountModel? ToModel(byte[] stream){
    IsChanged = true;
    if(stream[0] != accountIdentity)
      return null;

    int checksum = BitConverter.ToUInt16(stream, 1);

    if(checksum != (ushort) stream.Length + 1 - sizeof(ushort))
      return null;


    string payload = Encoding.UTF8.GetString(stream, sizeof(ushort) + 1, checksum - 1);
    model = new AccountModel();
    string[] splittedPayload = payload.Split(',');
    model.Username = splittedPayload[0].Trim();
    model.Password = splittedPayload[1].Trim();
    model.Activity = int.Parse(splittedPayload[2].Trim());

    return model;
  }


  const byte accountIdentity = 0xA1;

  //- Base model
  public class AccountModel{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int Activity { get; set; }
  }

  bool IsChanged;
  public AccountModel? model { get; set; }
  public byte[]? Stream { get; private set; }
}