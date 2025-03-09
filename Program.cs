using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("1. Mã hóa file");
        Console.WriteLine("2. Giải mã file");
        int choice = int.Parse(Console.ReadLine());

        if (choice == 1)
        {
            Console.Write("Nhập file cần mã hóa: ");
            string inputFile = Console.ReadLine();
            Console.Write("Nhập file lưu kết quả: ");
            string encryptedFile = Console.ReadLine();
            Console.Write("Nhập file lưu khóa: ");
            string keyFile = Console.ReadLine();

            var (key, iv) = AESCrypto.GenerateAESKey();
            AESCrypto.EncryptFile(inputFile, encryptedFile, key, iv);
            FileHandler.SaveKeyAndIV(keyFile, key, iv);

            Console.WriteLine("Mã hóa thành công!");
        }
        else if (choice == 2)
        {
            Console.Write("Nhập file mã hóa: ");
            string encryptedFile = Console.ReadLine();
            Console.Write("Nhập file đầu ra: ");
            string outputFile = Console.ReadLine();
            Console.Write("Nhập file chứa khóa: ");
            string keyFile = Console.ReadLine();

            var (key, iv) = FileHandler.LoadKeyAndIV(keyFile);
            AESCrypto.DecryptFile(encryptedFile, outputFile, key, iv);

            Console.WriteLine("Giải mã thành công!");
        }
    }
}
