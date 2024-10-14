namespace Service;
public interface ISerialization {
  public byte[] Serialize();
  public void Unserialize(byte[] raw);
}