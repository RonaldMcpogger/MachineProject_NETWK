using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class TCPClient : MonoBehaviour
{
    public string serverIP = "127.0.0.1"; // Set this to your server's IP address.
    public int serverPort = 4000;             // Set this to your server's port.
    [SerializeField] private string messageToSend = "0"; // The message to send.

    private TcpClient client;
    private NetworkStream stream;
    private Thread clientReceiveThread;

    private bool guess = true;
    public static string Guess = string.Empty;
    public static string Judgement = string.Empty;
    public static bool update = false;

    public GameObject MenuHandlerObject;
    public GameObject GameHandlerObject;

    void Start()
    {

    }

    void Update()
    {

    }

    void ConnectToServer()
    {
        try
        {
            client = new TcpClient(serverIP, serverPort);
            stream = client.GetStream();
            Debug.Log("Connected to server.");

            GameHandlerObject.GetComponent<MenuHandler>().Set();

            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (SocketException e)
        {
            Debug.LogError("SocketException: " + e.ToString());
        }
    }

    private void ListenForData()
    {
        try
        {
            byte[] bytes = new byte[1024];
            while (true)
            {
                // Check if there's any data available on the network stream
                if (stream.DataAvailable)
                {
                    int length;
                    // Read incoming stream into byte array.
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incomingData = new byte[length];
                        Array.Copy(bytes, 0, incomingData, 0, length);
                        // Convert byte array to string message.
                        string serverMessage = Encoding.UTF8.GetString(incomingData);
                        Debug.Log("Server message received: " + serverMessage);

                        if (guess)
                        {
                            Guess = serverMessage;
                            guess = false;
                        }
                        else
                        {
                            Judgement = serverMessage;
                            guess = true;
                            update = true;

                            
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    public void SendMessageToServer(string message)
    {
        if (client == null || !client.Connected)
        {
            Debug.LogError("Client not connected to server.");
            return;
        }

        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);
        Debug.Log("Sent message to server: " + message);
    }

    public void ConnectServer(string IP, int port)
    {
        serverIP = IP;
        serverPort = port;
        ConnectToServer();
    }

    public bool TryConnectServer(string IP, int port)
    {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(IP, port);
                    client.Close();
                    return true;
                }
                
            }
            catch (SocketException)
            {
                return false;
            }
    }

    void OnApplicationQuit()
    {
        if (stream != null)
            stream.Close();
        if (client != null)
            client.Close();
        if (clientReceiveThread != null)
            clientReceiveThread.Abort();
    }
}
