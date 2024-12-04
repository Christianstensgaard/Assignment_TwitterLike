using System.Security.Cryptography;
using RabbitMqDefault.interfaces;

public class EncryptionSidecar : ISidecar
{

  public EncryptionSidecar(){}


    private readonly byte[] _encryptionKey = [33, 112, 98, 109, 101, 105, 114, 105, 115, 97, 117, 116, 111, 110, 103, 100, 93, 88, 82, 84, 87, 79, 80, 86, 71, 73, 69, 83];
    private readonly Aes _aes = CreateAes();

    public byte[] Encrypt(byte[] data)
    {
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, _aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(data, 0, data.Length);
                cs.FlushFinalBlock();
            }
            return ms.ToArray();
        }
    }

    public byte[] Decrypt(byte[] encryptedData)
    {
        using (var ms = new MemoryStream(encryptedData))
        {
            using (var cs = new CryptoStream(ms, _aes.CreateDecryptor(), CryptoStreamMode.Read))
            {
                var decryptedData = new byte[ms.Length];
                cs.Read(decryptedData, 0, decryptedData.Length);
                return decryptedData;
            }
        }
    }

    private static Aes CreateAes()
    {
        var aes = Aes.Create();
        aes.KeySize = 256;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.None;
        using (var rng = RandomNumberGenerator.Create())
        {
            var iv = new byte[16];
            rng.GetBytes(iv);
            aes.IV = iv;
        }
        return aes;
    }

    private static byte[] GenerateEncryptionKey()
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            var key = new byte[32];
            rng.GetBytes(key);
            return key.Take(16).ToArray();
        }
    }

  public void Initialize()
  {
    //- Maybe Read the key from the database
    //- 
  }
}
