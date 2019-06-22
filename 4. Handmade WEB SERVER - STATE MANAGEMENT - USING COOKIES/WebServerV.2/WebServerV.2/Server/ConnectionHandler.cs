namespace WebServerV._2.Server
{
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    using WebServerV._2.Server.Common;
    using WebServerV._2.Server.Handlers;
    using WebServerV._2.Server.Http;
    using WebServerV._2.Server.Http.Contracts;
    using WebServerV._2.Server.Routing.Contracts;

    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IServerRouteConfig serverRouteConfig;

        public ConnectionHandler(Socket client, IServerRouteConfig serverRouteConfig)
        {
            CoreValidator.ThrowIfNull(client,nameof(client));
            CoreValidator.ThrowIfNull(serverRouteConfig, nameof(serverRouteConfig));

            this.client = client;
            this.serverRouteConfig = serverRouteConfig;
        }

        public async Task ProcessRequestAsync()
        {
            var request = await this.ReadRequest();

            var httpContext = new HttpContext(request);

            var response = new HttpHandler(this.serverRouteConfig).Handle(httpContext);

            ArraySegment<byte> toBytes = new ArraySegment<byte>(Encoding.UTF8.GetBytes(response.ToString()));

            await this.client.SendAsync(toBytes, SocketFlags.None);

            Console.WriteLine($"------REQUEST------");
            Console.WriteLine(request);
            Console.WriteLine($"------RESPONSE------");
            Console.WriteLine(response.ToString());
            Console.WriteLine();

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<IHttpRequest> ReadRequest()
        {
            var request = new StringBuilder();
            ArraySegment<byte> data = new ArraySegment<byte>(new byte[1024]);


            while (true)
            {
                int numberOfBytesRead = await this.client.ReceiveAsync(data, SocketFlags.None);

                if (numberOfBytesRead == 0)
                {
                    break;
                }
                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);
                request.AppendLine(bytesAsString);

                if (numberOfBytesRead < 1023) break;
            }
            return new HttpRequest(request.ToString());
        }
    }
}
