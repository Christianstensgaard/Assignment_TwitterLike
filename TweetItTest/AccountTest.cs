using System.Text;
using RabbitMqDefault.Converter;

namespace TweetItTest;

public class AccountTest
{
  public static RabbitMqDefault.Converter.AccountConverter.AccountModel  Model = new(){
    Username = "UserDemo",
    Password = "PassDemo",
    Activity = 1
  };

  ushort signedChecksum = (ushort)(Encoding.UTF8.GetBytes($"{Model.Username},{Model.Password},{Model.Activity}").Length + 1);

  AccountConverter accountConverter = new AccountConverter();
    [Fact]
    public void AccountConverter()
    {
      byte[] testStream =  accountConverter.ToStream(Model);
      ushort checksum = BitConverter.ToUInt16(testStream, 1);


      Assert.Equal(0xA1, testStream[0]);
      Assert.Equal(signedChecksum, checksum);

      //- Test the string parsing
      AccountConverter.AccountModel? outputModel = accountConverter.ToModel(testStream);
      Assert.True(outputModel != null);


      Assert.True(outputModel.Username == Model.Username);
      Assert.True(outputModel.Password == Model.Password);
      Assert.True(outputModel.Activity == Model.Activity);

    }
}