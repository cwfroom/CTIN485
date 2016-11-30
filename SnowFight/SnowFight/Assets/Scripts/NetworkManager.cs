/*

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.Net.Sockets;

public class NetworkManager : MonoBehaviour
{
    [Serializable]
    public struct MessageBase {
       public string str;
        //int pid;
       public float f0, f1, f2, f3, f4, f5;
    }


    GameManager gmr;

    const string host = "127.0.0.1";
    //const string host = "104.236.7.195";
    const int port = 10086;
    const int buffer_size = 1024;

    private bool clientReady = false;

    private Socket client_socket;
    private byte[] buffer;

    //AsyncCallback receiveAsyncCall;

    public readonly static Queue<Action> ExecuteOnMainThread = new Queue<Action>();

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gmr = GetComponent<GameManager>();
  
    }

    // Update is called once per frame
    void Update()
    {
        // dispatch stuff on main thread
        while (ExecuteOnMainThread.Count > 0)
        {
            ExecuteOnMainThread.Dequeue().Invoke();
        }
    }

    private static byte[] MessageSerialize(string str, Vector3 vec, Vector3 vec2)
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

    private static void MessageDeserialize(byte[] msg, out string str, out Vector3 vec, out Vector3 vec2)
    {
        Debug.Log(msg);

        MemoryStream ms = new MemoryStream(msg);
        BinaryFormatter formatter = new BinaryFormatter();
        MessageBase mb = (MessageBase)formatter.Deserialize(ms);

        
        Debug.Log(mb.str);

        str = mb.str;
        vec.x = mb.f0;
        vec.y = mb.f1;
        vec.z = mb.f2;
        vec2.x = mb.f3;
        vec2.y = mb.f4;
        vec2.z = mb.f5;
    }

    public void Connect()
    {
        client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //IPEndPoint remoteAddr = new IPEndPoint(IPAddress.Parse(host), port);
        Debug.Log("Connecting to " + host + ":" + port);
        client_socket.Connect(host,port);
        buffer = new byte[buffer_size];
        clientReady = true;

        Thread ctThread = new Thread(Listen);
        ctThread.Start();
        
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
            MessageDeserialize(buffer, out msg, out vec, out vec2);
            ParseMessage(msg, vec, vec2);
        }
    }

    public void SendString(string str)
    {
        Send(MessageSerialize(str, new Vector3(), new Vector3()));
    }

    public void SendVector(string str, Vector3 vec)
    {
        Send(MessageSerialize(str, vec, new Vector3()));
    }

    public void SendVector(string str, Vector3 vec, Vector3 vec2)
    {
        Send(MessageSerialize( str, vec, vec2));
    }

    public void DisConnect()
    {
        clientReady = false;
        client_socket.Close();
    }

    private void ParseMessage(string msg, Vector3 vec, Vector3 vec2)
    {
        string[] results = msg.Split(':');
        string command = results[0];
        Debug.Log(command);

        string value = results[1];
        switch (command) {
            case "PID":
                gmr.SetPID(Convert.ToInt32(value));
                break;
            case "START":
                ExecuteOnMainThread.Enqueue(() => { StartCoroutine(StartGameMainThread()); });       
                break;
            case "POS":
                ExecuteOnMainThread.Enqueue(() => { StartCoroutine(ReceivePosMainThread(vec)); });
                break;
        }
        
        Debug.Log(command);
        Debug.Log(value);

    }

    IEnumerator StartGameMainThread()
    {
        yield return null;
        gmr.LoadLevel();
    }
    
    IEnumerator ReceivePosMainThread(Vector3 vec)
    {
        yield return null;
        gmr.ReceivePos(vec);
    }
    
    void OnApplicationQuit()
    {
        DisConnect();
    }


}

*/