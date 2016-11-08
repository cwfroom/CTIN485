using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour
{
    GameManager gmr;

    const string host = "127.0.0.1";
    const int port = 10086;

    private bool clientReady = false;
    private TcpClient client;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;

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

    public void Connect()
    {
        try
        {
            client = new TcpClient(host, port);
            stream = client.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            clientReady = true;

            Thread ctThread = new Thread(Listen);
            ctThread.Start();

        }catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
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
                Debug.Log("Raw Message: " + msg + "\n");
                ParseMessage(msg);
            }
        }
        
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

    IEnumerator StartGameMainThread()
    {
        yield return null;
        gmr.LoadLevel();
    }


    
}
