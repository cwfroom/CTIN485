using System;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace SFServer
{
    class ClientHandler {
        private bool clientReady = false;
        private TcpClient client;
        private NetworkStream stream;
        private StreamWriter writer;
        private StreamReader reader;

        public ClientHandler(TcpClient inClient, int pid)
        {
            this.client = inClient;
            stream = client.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            clientReady = true;

            Thread ctThread = new Thread(Listen);
            ctThread.Start();

            Send("PID:" + pid);
        }

        public void Send(string str)
        {
            if (clientReady)
            {
                writer.Write(str + "\r\n");
                writer.Flush();
            }
        }

        public void Listen()
        {
            while (clientReady)
            {
                if (stream.DataAvailable)
                {
                    string msg = reader.ReadLine();
                    Console.Write("Raw Message: " + msg + "\n");
                }
            }

        }
        
    }

    class Server
    {
        const string host = "127.0.0.1";
        const int port = 10086;

        private int client_count = 0;
        private TcpListener listener;
        List<ClientHandler> clients;

        public Server()
        {
            clients = new List<ClientHandler>();
            listener = new TcpListener(IPAddress.Parse(host),port);
            listener.Start();
            Console.Write("Starting server at " + host + " port " + port + "\n");
            
            while (true)
            {
                TcpClient client_socket = listener.AcceptTcpClient();
                ClientHandler c = new ClientHandler(client_socket, client_count);
                clients.Add(c);
                Console.Write("Connection from " + client_socket.Client.RemoteEndPoint.ToString() + "\n");

                client_count++;
                if (client_count == 1)
                {
                    StartSession();
                    //client_count++;        
                }
            }
        }

        public void StartSession()
        {
            foreach (ClientHandler ch in clients)
            {
                Console.Write("Starting session");
                ch.Send("START:");
            }
        }


    }
}
