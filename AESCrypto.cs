using System;
using System.IO;
using System.Security.Cryptography;

public class AESCrypto
{
    private static readonly int KeySize = 256; // 256-bit key
    private static readonly int BlockSize = 128; // 128-bit block size

    public static void EncryptFile(string inputFile, string outputFile, byte[] key, byte[] iv)
    {
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = KeySize;
            aes.BlockSize = BlockSize;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key;
            aes.IV = iv;

            using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
            using (CryptoStream cryptoStream = new CryptoStream(fsOutput, aes.CreateEncryptor(), CryptoStreamMode.Write))
            using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
            {
                fsInput.CopyTo(cryptoStream);
            }
        }
    }
    public static void DecryptFile(string inputFile, string outputFile, byte[] key, byte[] iv)
    {
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = KeySize;
            aes.BlockSize = BlockSize;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key;
            aes.IV = iv;

            using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
            using (CryptoStream cryptoStream = new CryptoStream(fsInput, aes.CreateDecryptor(), CryptoStreamMode.Read))
            using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
            {
                cryptoStream.CopyTo(fsOutput);
            }
        }
    }
    public static (byte[], byte[]) GenerateAESKey()
    {
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = KeySize;
            aes.BlockSize = BlockSize;
            aes.GenerateKey();
            aes.GenerateIV();
            return (aes.Key, aes.IV);
        }
    }

}
