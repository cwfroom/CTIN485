﻿using UnityEngine;
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
    GameManager gmr;

    //const string host = "127.0.0.1";
    const string host = "104.236.7.195";
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

    private static byte[] MessageSerialize(int type, string str, Vector3 vec)
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

    private static int MessageDeserialize(byte[] msg, out string str, out Vector3 vec)
    {
        MemoryStream ms = new MemoryStream(msg);
        BinaryFormatter formatter = new BinaryFormatter();
        int msgType = (int)formatter.Deserialize(ms);
        str = (string)formatter.Deserialize(ms);
        if (msgType > 0)
        {
            vec.x = (float)formatter.Deserialize(ms);
            vec.y = (float)formatter.Deserialize(ms);
            vec.z = (float)formatter.Deserialize(ms);
        }
        else
        {
            vec = Vector3.zero;
        }
        return msgType;
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
            int type = MessageDeserialize(buffer, out msg, out vec);
            if (type == 0)
            {
                ParseMessage(msg);
            }else
            {
                ParseMessage(msg, vec);
            }
        }
        
    }

    public void SendString(string str)
    {
        Send(MessageSerialize(0, str, new Vector3()));
    }

    public void SendVector(string str, Vector3 vec)
    {
        Send(MessageSerialize(1,str, vec));
    }


    public void DisConnect()
    {
        clientReady = false;
        client_socket.Close();
    }

    private void ParseMessage(string msg)
    {
        string[] results = msg.Split(':');
        string command = results[0];
        string value = results[1];
        switch (command) {
            case "PID":
                gmr.SetPID(Convert.ToInt32(value));
                break;
            case "START":
                ExecuteOnMainThread.Enqueue(() => { StartCoroutine(StartGameMainThread()); });       
                break;
        }
        
        Debug.Log(command);
        Debug.Log(value);

    }

    private void ParseMessage(string msg, Vector3 vec)
    {
        switch (msg) {
            case "POS":
                ExecuteOnMainThread.Enqueue(() => { StartCoroutine(ReceivePosMainThread(vec)); });
                break;
            
        }

        
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
