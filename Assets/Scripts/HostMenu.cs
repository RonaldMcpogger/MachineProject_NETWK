using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class HostMenu : MonoBehaviour
{
    [SerializeField] public TMP_InputField IP;
    [SerializeField] public TMP_InputField Port;
    [SerializeField] public GameObject INVALIDIP;
    [SerializeField] public GameObject INVALIDPORT;


    [SerializeField] public GameObject serverObject;
    [SerializeField] public GameObject clientObject;
    // Start is called before the first frame update
    void Start()
    {
        INVALIDIP.SetActive(false);
        INVALIDPORT.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryStartServer()
    {
        INVALIDIP.SetActive(false);
        INVALIDPORT.SetActive(false);

        IPAddress ip;
        int port = int.Parse(Port.text);
        string[] periods = IP.text.Split('.');
        if (port < 1 || port > 65535)
            INVALIDPORT.SetActive(true);

        if (!IPAddress.TryParse(IP.text, out ip) || 
            IP.text == string.Empty || IP.text == null ||
            periods.Length != 4)
        {
            INVALIDIP.SetActive(true);
        }

        if (port >= 1 && port <= 65535 &&
            IPAddress.TryParse(IP.text, out ip) &&
            IP.text != string.Empty && IP.text != null &&
            periods.Length == 4)
        {
            Debug.Log("Start Server!");
            serverObject.GetComponent<TCPServer>().serverIP = IP.text;
            serverObject.GetComponent<TCPServer>().serverPort = port;

            serverObject.GetComponent<TCPServer>().StartServer();

            GameHandler2.PlayerNumber = 1;
            clientObject.GetComponent<TCPClient>().ConnectServer(IP.text, port);
        }

    }
}
