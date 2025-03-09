using System;
using System.IO;
using System.Net.Sockets;

public class NetworkHandler
{
    public static void SendFile(string filePath, string ip, int port)
    {
        TcpClient client = new TcpClient(ip, port);
        using (NetworkStream stream = client.GetStream())
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            fs.CopyTo(stream);
        }
        client.Close();
    }

    public static void ReceiveFile(string savePath, int port)
    {
        TcpListener listener = new TcpListener(System.Net.IPAddress.Any, port);
        listener.Start();
        using (TcpClient client = listener.AcceptTcpClient())
        using (NetworkStream stream = client.GetStream())
        using (FileStream fs = new FileStream(savePath, FileMode.Create))
        {
            stream.CopyTo(fs);
        }
        listener.Stop();
    }
}
