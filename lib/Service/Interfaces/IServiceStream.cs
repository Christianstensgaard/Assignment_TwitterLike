namespace Service.Interfaces;
public interface IServiceStream{
  public byte[] Serialize();
  public void UnSerialize(byte[] stream);
}