using System.Net;
using System.Net.Sockets;
using System.Text;

const string ipAddress = "127.0.0.1"; // ip address where the listener works
var port = 2245; // port where the listener listens

while (true)
{
    Console.WriteLine("Type data that you want to send: ");
    Console.Write("> ");
    var userData = Console.ReadLine();

    if (userData?.ToLower() == "exit")
        break;

    Send(port, ipAddress, userData);
}

void Send(int serverPort, string serverIp, string? dataToSend)
{
    try
    {
        // create new socket to communicate with
        var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // connect with the server
        var serverIpAddress = IPAddress.Parse(serverIp);
        var serverEndPoint = new IPEndPoint(serverIpAddress, serverPort);
        clientSocket.Connect(serverEndPoint);

        if (string.IsNullOrWhiteSpace(dataToSend))
            dataToSend = "Default value of data to send";
        
        // convert data to byte[]
        var dataBuffer = Encoding.ASCII.GetBytes(dataToSend);
    
        clientSocket.Send(dataBuffer);

        Console.WriteLine("Data has been sent.\n");
    
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception has been thrown: {ex.Message}\n");
    }
}