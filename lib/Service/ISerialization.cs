namespace Service;
public interface ISerialization{
  public byte[] Serialize();
  public T Unserialize<T>(byte[] raw);
}