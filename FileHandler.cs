using System;
using System.IO;

public class FileHandler
{
    public static void SaveKeyAndIV(string keyFile, byte[] key, byte[] iv)
    {
        using (FileStream fs = new FileStream(keyFile, FileMode.Create))
        using (BinaryWriter writer = new BinaryWriter(fs))
        {
            writer.Write(key.Length);
            writer.Write(key);
            writer.Write(iv.Length);
            writer.Write(iv);
        }
    }

    public static (byte[], byte[]) LoadKeyAndIV(string keyFile)
    {
        using (FileStream fs = new FileStream(keyFile, FileMode.Open))
        using (BinaryReader reader = new BinaryReader(fs))
        {
            int keyLen = reader.ReadInt32();
            byte[] key = reader.ReadBytes(keyLen);
            int ivLen = reader.ReadInt32();
            byte[] iv = reader.ReadBytes(ivLen);
            return (key, iv);
        }
    }
}
