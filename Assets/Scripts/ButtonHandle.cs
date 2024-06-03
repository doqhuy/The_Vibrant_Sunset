using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandle : MonoBehaviour
{
    public Button Host;
    public Button Client;
    public Button Server;
    public Button Stop;

    public TMP_InputField IP;
    public TMP_InputField Port;

    public string IPHOST;

    public NetworkManager NetworkManager;
    public UnityTransport UnityTransport;
    void Start()
    {
        Host.onClick.RemoveAllListeners();
        Host.onClick.AddListener(() => { NetworkManager.StartHost(); });
        Client.onClick.RemoveAllListeners();
        Client.onClick.AddListener(() => { NetworkManager.StartClient(); });
        Server.onClick.RemoveAllListeners();
        Server.onClick.AddListener(() => { NetworkManager.StartServer(); });
        Stop.onClick.RemoveAllListeners();
        Stop.onClick.AddListener(()=>NetworkManager.Shutdown());
    }

    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }

    // Update is called once per frame
    void Update()
    {
        UnityTransport.ConnectionData.Address = IP.text;
        UnityTransport.ConnectionData.Port = ushort.Parse(Port.text);
        IPHOST = GetLocalIPAddress();
    }
}
