using Tools;

namespace Service;
public class ServiceConfig{
  public ServiceConfig(){
    Init();
  }
  public ServiceConfig(string serviceName, int type, bool tracing, bool logging){
    Init();
  }

  public byte[]? Config {get; private set;} = null;
  public void Tracing(bool enable){
    SetBit(const_enableTracing, enable);
  }
  public void Logging(bool enable){
    SetBit(const_enableLogging, enable);
  }
  public void Type(int type){
    //- Set the Type of the service. will not be used this time.
    SetBit(ServiceType, false);
  }
  public void ServiceName(string name){
    Name = name;
  }
  public void SetSerializationHandler(ISerialization serialization){
    Serialization = serialization;
  }


  //PRIVATE
  void SetBit(int type, bool value){
    if(Config == null)
      return;
    BitHandler.SetBit(Config, type, value);
  }
  void Init(){
    Config = new byte[const_configSize];
  }

  string Name { get; set; } = string.Empty;
  byte ServiceType { get; set; } = 0x00;
  byte[] ServiceMessageId = new byte[1]; //- gives a range of 0-255 used by the  server to remove some overhead.
  ISerialization? Serialization {get; set;} = null;

  //- Const indexing of bits
  const int const_configSize = 10;
  const int const_enableLogging = 2;
  const int const_enableTracing = 3;
  const int const_serviceType = 4;
}