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

public class JoinMenu : MonoBehaviour
{
    [SerializeField] public TMP_InputField IP;
    [SerializeField] public TMP_InputField Port;
    [SerializeField] public GameObject INVALIDIP;
    [SerializeField] public GameObject INVALIDPORT;
    [SerializeField] public GameObject JoinFailed;


    [SerializeField] public GameObject clientObject;
    // Start is called before the first frame update
    void Start()
    {
        INVALIDIP.SetActive(false);
        INVALIDPORT.SetActive(false);
        JoinFailed.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryJoinServer()
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
            Debug.Log("Join Server!");

            if(clientObject.GetComponent<TCPClient>().TryConnectServer(IP.text, port))
            {
                GameHandler2.PlayerNumber = 2;
                clientObject.GetComponent<TCPClient>().ConnectServer(IP.text, port);
            }
            else
            {
                JoinFailed.SetActive(true);
            }

        }

    }
}
