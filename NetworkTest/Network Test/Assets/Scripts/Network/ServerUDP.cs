using System.Net.Sockets;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Text;
using UnityEngine.Networking;

public class ServerUDP : MonoBehaviour {
    
    bool receivedIP;
    string receivedStringIP;
    bool connected;
    //public NetworkLobbyManager networkManager;

    public bool directConnect;
    public string serverIP;

	// Update is called once per frame
	void Update () {
        //if (receiving) { StartReceivingIP(); }
        if (receivedIP && !connected) { SetupConnection(receivedStringIP); receivedIP = false; }
	}

    UdpClient sender;
    int remotePort = 19784;
    int localPort = 19785;
    void Start()
    {
        //SendData ();
        receivedIP = false;
        connected = false;

    }

    public void StartListener()
    {

        StartReceivingIP();
    }

    public void StartSendingIP()
    {
        //GetComponent<NetworkLobbyManager>().StartHost();
        sender = new UdpClient(localPort, AddressFamily.InterNetwork);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Broadcast, remotePort);
        sender.Connect(groupEP);
        InvokeRepeating("SendData", 0, 1f);
    }

    void SendData()
    {

      //  Debug.Log("SendData");
        string customMessage = "InterPlaycesTest" + "*" + Network.player.ipAddress;

        if (customMessage != "")
        {
            sender.Send(Encoding.ASCII.GetBytes(customMessage), customMessage.Length);
        }
    }

    UdpClient receiver;

    public void StartReceivingIP()
    {
        try
        {
            if (receiver == null)
            {
                receiver = new UdpClient(remotePort);
                receiver.BeginReceive(new AsyncCallback(ReceiveData), null);
            }
        }
        catch (SocketException e)
        {
            Debug.Log("Socket Exception (ServerUDP):" + e.Message);
        }
    }
    private void ReceiveData(IAsyncResult result)
    {
        //
        IPEndPoint receiveIPGroup = new IPEndPoint(IPAddress.Any, remotePort);
        byte[] received;
        if (receiver != null)
        {
            received = receiver.EndReceive(result, ref receiveIPGroup);
            //receiver.Close();
        }
        else
        {
            return;
        }
        receiver.BeginReceive(new AsyncCallback(ReceiveData), null);
        string receivedString = Encoding.ASCII.GetString(received);

       // Debug.Log("Received: " + receivedString);
        receivedStringIP = receivedString;
        receivedIP = true;
        
    }

    public void DirectConnect()
    {
        try
        {
            GetComponent<NetworkLobbyManager>().networkAddress = serverIP;
            GetComponent<NetworkLobbyManager>().networkPort = 7777;
            GetComponent<NetworkLobbyManager>().StartClient();
            connected = true;
        }
        catch
        {
            Debug.Log("No connection possible");
        }
    }

    private void SetupConnection(string IPString)
    {
        //Parse String
        char[] seperator = new char[1];
        seperator[0] = '*';

        string[] SplitIP = IPString.Split(seperator);

            Debug.Log("IP = " + SplitIP[1]);
        //Stop UDP stuff (on all clients?)
        //Connect to server

        //Network.Connect(SplitIP[1], 7777);

        try
        {
            GetComponent<NetworkLobbyManager>().networkAddress = SplitIP[1];
            GetComponent<NetworkLobbyManager>().networkPort = 7777;
            GetComponent<NetworkLobbyManager>().StartClient();
            connected = true;
        }
        catch
        {
            Debug.Log("No connection possible");
        }
    }
}
