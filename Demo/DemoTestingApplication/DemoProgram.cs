// See https://aka.ms/new-console-template for more information
using Service;
using Service.Tools;

byte[] stream = new byte[1024];


AccountModel target = new AccountModel(){
  AccountName = "Admin",
  Password    = "Admin",
};

stream = target.Serialize();



AccountModel result = AccountModel.UnPack(stream);




System.Console.WriteLine(result.AccountName);
System.Console.WriteLine(result.Password);




class AccountModel : ISerialization
{
  public static AccountModel UnPack(byte[] stream){
    using MemoryStream Stream = new MemoryStream(stream);
    using BinaryReader reader = new BinaryReader(Stream);

    return new AccountModel(){
      AccountName = reader.ReadString(),
      Password    = reader.ReadString()
    };
  }


  public string AccountName { get; set; }
  public string Password { get; set; }



    public byte[] Serialize()
    {
      using MemoryStream stream = new MemoryStream();
      using BinaryWriter write = new BinaryWriter(stream);

      write.Write(AccountName);
      write.Write(Password);

      return stream.GetBuffer();
    }

    public void Unserialize(byte[] raw)
    {
        throw new NotImplementedException();
    }

}



