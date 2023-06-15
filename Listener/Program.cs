using System.Net;
using System.Net.Sockets;
using System.Text;

var port = 2245;

while(true)
    Listen(port);

void Listen(int portToListen)
{
    try
    {
        // create new socket
        var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        var ipAddressToListen = IPAddress.Any; // accept any ip address that communicates with selected port
    
        var localEndPoint = new IPEndPoint(ipAddressToListen, portToListen);
        listener.Bind(localEndPoint);

        // listen on socket
        listener.Listen(10); // set max number of awaiting connections

        Console.WriteLine($"Listening on port {portToListen}...");

        while (true)
        {
            // accept connections
            var clientSocket = listener.Accept();

            // get clients connection ip // from where the connections comes
            var remoteClientIp = (((IPEndPoint)clientSocket.RemoteEndPoint!)!).Address;
        
            // get data as byte[] and convert them to string
            var buffer = new byte[1024];
            var bytesRead = clientSocket.Receive(buffer);
            var receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Data from connection {remoteClientIp}: {receivedData}");

            // -------------------------
            //     Work on got data
            // -------------------------

            // close connection
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Exception has been thrown: " + ex.Message);
    }
}