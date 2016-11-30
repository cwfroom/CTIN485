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
    
    public struct Vector3
    {
        public float x, y, z;
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    };

    [Serializable]
    struct MessageBase
    {
        public string str;
        //int pid;
        public float f0, f1, f2, f3, f4, f5;
    }


    class Serializer
    {
        public static byte[] MessageSerialize(string str, Vector3 vec, Vector3 vec2)
        {
            MessageBase mb = new MessageBase();
            mb.str = str;
            mb.f0 = vec.x;
            mb.f1 = vec.y;
            mb.f2 = vec.z;
            mb.f3 = vec2.x;
            mb.f4 = vec2.y;
            mb.f5 = vec2.z;

            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, mb);
            return ms.ToArray();
        }

        public static void MessageDeserialize(byte[] msg, out string str, out Vector3 vec, out Vector3 vec2)
        {
            MemoryStream ms = new MemoryStream(msg);
            BinaryFormatter formatter = new BinaryFormatter();
            MessageBase mb = (MessageBase)formatter.Deserialize(ms);
            str = mb.str;
            vec.x = mb.f0;
            vec.y = mb.f1;
            vec.z = mb.f2;
            vec2.x = mb.f3;
            vec2.y = mb.f4;
            vec2.z = mb.f5;
        }

    }
    class ClientHandler
        {

            private byte[] buffer;
            const int buffer_size = 1024;

            private bool clientReady = false;

            private Server server;
            private Socket client_socket;
            public int pid;

            public ClientHandler(Server s,Socket inClient, int pid)
            {
                buffer = new byte[buffer_size];
                this.server = s;
                this.client_socket = inClient;
                this.pid = pid;
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
                    string msg;
                    Vector3 vec;
                    Vector3 vec2;
                    Serializer.MessageDeserialize(buffer, out msg, out vec, out vec2);
                    ParseMessage(msg, vec, vec2);
            }

            }

            public void SendString(string str)
            {
                Send(Serializer.MessageSerialize(str, new Vector3(), new Vector3()));
            }

            private void ParseMessage(string msg, Vector3 vec, Vector3 vec2)
            {
                string[] result = msg.Split(':');
                string command = result[0];
                if (result.Length > 0)
                {
                    string value = result[1];
                }

                switch (command)
                {
                    case "POS": 
                    server.SendToExcept(Serializer.MessageSerialize("POS", vec, new Vector3()), pid);
                        break;

                }
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
                server_socket.Bind(new IPEndPoint(IPAddress.Any, port));
                server_socket.Listen(10);
                Console.Write("Starting listening at port " + port + "\n");

                while (true)
                {
                    Socket client_socket = server_socket.Accept();

                    ClientHandler c = new ClientHandler(this,client_socket, client_count);
                    clients.Add(c);
                    Console.Write("Connection from " + client_socket.RemoteEndPoint.ToString() + "\n");

                    client_count++;
                    if (client_count >= 1)
                    {
                        StartSession();       
                    }
                }
            }

            public void StartSession()
            {
                foreach (ClientHandler ch in clients)
                {
                    Console.Write("Starting session \n");
                    SendToAll(Serializer.MessageSerialize("START:", new Vector3(0,0,0), new Vector3(0,0,0)));
                }
            }

            public void SendToAll(byte[] data)
            {
                foreach (ClientHandler ch in clients)
                {
                    ch.Send(data);
                }
            }

            public void SendToExcept(byte[] data, int pid)
            {
                foreach (ClientHandler ch in clients)
                {
                    if (ch.pid != pid)
                    {
                        ch.Send(data);
                    }
                }
            } 
        
        }
}


