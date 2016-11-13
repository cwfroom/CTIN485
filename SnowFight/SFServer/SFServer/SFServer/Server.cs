using System;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace SFServer
{
    public struct Vector3 {
        public float x, y, z;
    }


    class Serializer {
        public static byte[] MessageSerialize(int type, string str, Vector3 vec)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(ms, type);

            switch (type)
            {
                case 0:
                    bf.Serialize(ms, str);
                    break;
                case 1:
                    bf.Serialize(ms, str);
                    bf.Serialize(ms, vec.x);
                    bf.Serialize(ms, vec.y);
                    bf.Serialize(ms, vec.z);
                    break;
            }

            return ms.ToArray();
        }

        public static Vector3 MessageDeserialize(byte[] msg, out string str)
        {
            MemoryStream ms = new MemoryStream(msg);
            BinaryFormatter formatter = new BinaryFormatter();
            int msgType = (int)formatter.Deserialize(ms);
            str = (string)formatter.Deserialize(ms);
            Vector3 vec = new Vector3();
            if (msgType > 0)
            {
                vec.x = (float)formatter.Deserialize(ms);
                vec.y = (float)formatter.Deserialize(ms);
                vec.z = (float)formatter.Deserialize(ms);
                return vec;
            }
            return vec;
        }
    }

    
    class ClientHandler {

        private byte[] buffer;
        const int buffer_size = 1024;
        
        private bool clientReady = false;

        private Socket client_socket;


        public ClientHandler(Socket inClient, int pid)
        {
            buffer = new byte[buffer_size];
            this.client_socket = inClient;
            clientReady = true;

            Thread ctThread = new Thread(Listen);
            ctThread.Start();

            SendString("PID:" + pid);
        }

        public void Send(byte[] data)
        {
            if (clientReady)
            {
                client_socket.Send(data);
            }
        }

        public void Listen()
        {
            while (clientReady)
            {
                client_socket.Receive(buffer);
                Console.Write(buffer);
            }

        }

        public void SendString(string str)
        {
            Send(Serializer.MessageSerialize(0, str, new Vector3()));
        }

        

    }

    class Server
    {
        const int port = 10086;

        private int client_count = 0;
        private Socket server_socket;

        
        List<ClientHandler> clients;

        public Server()
        {
            clients = new List<ClientHandler>();
            server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server_socket.Bind(new IPEndPoint(IPAddress.Loopback,port));
            server_socket.Listen(10);
            Console.Write("Starting listening at port " + port + "\n");
            
            while (true)
            {
                Socket client_socket = server_socket.Accept();
                
                ClientHandler c = new ClientHandler(client_socket, client_count);
                clients.Add(c);
                Console.Write("Connection from " + client_socket.RemoteEndPoint.ToString() + "\n");

                client_count++;
                if (client_count >= 1)
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
                //ch.Send("START:");
            }
        }


    }
}
