namespace _03.SimpleWebServer
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class StartUp
    {
        public static void Main()
        {
            var address = IPAddress.Parse("127.0.0.1");
            var port = 1300;
            var server = new TcpListener(address, port);
            server.Start();

            Console.WriteLine("Server started.");
            Console.WriteLine($"Listening to TCP clients at 127.0.0.1:{port}");

            var task = Task.Run(async () => await Connect(server));
            task.Wait();
        }

        private static async Task Connect(TcpListener listener)
        {
            while (true)
            {
                //wait for browser to connect
                Console.WriteLine("Waiting for client...");
                var client = await listener.AcceptTcpClientAsync();

                Console.WriteLine("Client connected");

                //read request
                var request = new byte[1024];
                await client.GetStream().ReadAsync(request, 0, request.Length);

                var message = Encoding.UTF8.GetString(request);
                Console.WriteLine(message);

                //send request
                var data = Encoding.UTF8.GetBytes("Hello from server!");
                await client.GetStream().WriteAsync(data, 0, data.Length);
                client.Dispose();
            }
        }
    }
}