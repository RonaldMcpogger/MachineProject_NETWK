using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class TCPServer : MonoBehaviour
{
    public string serverIP = "127.0.0.1";
    public int serverPort = 4000;

    TcpListener server = null;
    TcpClient client = null;
    NetworkStream stream = null;
    Thread thread;

    private bool guess = true;
    public string Guess = string.Empty;
    public string Judgement = string.Empty;

    public GameObject MenuHandlerObject;
    private MenuHandler Handler;

    private void Start()
    {
        Handler = MenuHandlerObject.GetComponent<MenuHandler>();

    }

    private void Update()
    {

    }

    private void SetupServer()
    {
        try
        {
            IPAddress localAddr = IPAddress.Parse(serverIP);
            server = new TcpListener(localAddr, serverPort);
            server.Start();

            byte[] buffer = new byte[1024];
            string data = null;

            while (true)
            {
                Debug.Log("Waiting for connection...");
                client = server.AcceptTcpClient();
                Debug.Log("Connected!");

                data = null;
                stream = client.GetStream();

                int i;

                while ((i = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    data = Encoding.UTF8.GetString(buffer, 0, i);
                    Debug.Log("Received: " + data);

                    if (guess)
                    {
                        Guess = data.ToString();
                        guess = false;
                    }
                    else
                    {
                        Judgement = data.ToString();
                        guess = true;
                    }

                    //string response = "Server response: " + data.ToString();
                    SendMessageToClient(data.ToString());
                }
                client.Close();
            }
        }
        catch (SocketException e)
        {
            Debug.Log("SocketException: " + e);
        }
        finally
        {
            server.Stop();
        }
    }

    private void OnApplicationQuit()
    {
        stream.Close();
        client.Close();
        server.Stop();
        thread.Abort();
    }

    public void StartServer()
    {
        thread = new Thread(new ThreadStart(SetupServer));
        thread.Start();
    }
    public void SendMessageToClient(string message)
    {
        byte[] msg = Encoding.UTF8.GetBytes(message);
        stream.Write(msg, 0, msg.Length);
        Debug.Log("Sent: " + message);
    }
}
