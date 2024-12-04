namespace RabbitMqDefault.interfaces;
public interface IParse{
  public byte[] ToByteArray();
  public void ToModel(byte[] stream);
}