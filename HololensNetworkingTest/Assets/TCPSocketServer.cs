using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using System.Runtime.Serialization;

public class TCPSocketServer : MonoBehaviour {

	System.Threading.Thread socketThread;
	volatile bool keepReading = false;

	private System.Object threadLocker = new System.Object();

	public Text output;
	bool listening = false;
	bool connected = false;
	IPAddress ip;

	public BroadcastingBehaviour broadcast;
	static ManualResetEvent _serverDone = new ManualResetEvent (false);
	const int TIMEOUT_MILLISECONDS = 10000;
	const int MAX_BUFFER_SIZE = 2048;

	private byte[] receiveBuffer = new byte[MAX_BUFFER_SIZE];

	private long lastTime;

	// Use this for initialization
	void Start () {
		output.text = ("Test");

		broadcast.StartBroadcasting ();
	
		startServer ();

	}

	void startServer() {
		socketThread = new System.Threading.Thread (networkCode);
		socketThread.IsBackground = true;
		socketThread.Start ();
	}

	private string getIPAddress() {
		IPHostEntry host;
		string localIP = "";
		host = Dns.GetHostEntry (Dns.GetHostName ());
		foreach (IPAddress ip in host.AddressList) {
			if (ip.AddressFamily == AddressFamily.InterNetwork) {
				localIP = ip.ToString ();
				break;
			}
		}
		return localIP;
	}


	// Update is called once per frame
	void Update () {
		
		//if (listening) {
			
		//} else 
		if (handler != null  && handler.Connected) {
			//Debug.Log ("Update - Connected");
			if (broadcast.broadcasting) {
				broadcast.StopBroadcasting ();
			}
			if (output != null) {
				output.text = "CONNECTED!!"; 
			}
		} else {
			if (ip != null) {
				string textToDisplay = string.Concat ("LISTENING: IP = ", ip.ToString ());
				if (output != null) {
					output.text = textToDisplay;
				}
				if (!broadcast.broadcasting) {
					broadcast.StartBroadcasting ();
				}
			}
		}
	}

	Socket listener;
	Socket handler;

	void networkCode() {
		string data;
		byte[] bytes = new Byte[1024];

		IPAddress[] ipArray = Dns.GetHostAddresses (getIPAddress ());
		Debug.Log ("IP Endpoint = " + ipArray [0] + " , Port = " + 1755);
		IPEndPoint localEndPoint = new IPEndPoint (ipArray [0], 1755);
		lock (threadLocker) {
			Debug.Log ("IP SET: " + ipArray[0]);
			ip = ipArray [0];
		}

		listener = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		while (true) {
			try {
				listener.Bind (localEndPoint);
				listener.Listen (10);
				while (true) {
					keepReading = true;
					lock (threadLocker) {
						connected = false;
						listening = true;
					}
					Debug.Log ("Waiting for Connection");

					handler = listener.Accept ();

					Debug.Log ("Client Connected");

					Message m = new Message ();
					m.commandID = (int)NetworkCommands.CmdSceneToGame;
					m.parameters = new string[0];
					Debug.Log("Message: " + m.commandID);
					Send (m);

					lock (threadLocker) {
						listening = false;
						connected = true;
					}

					handler.BeginReceive (receiveBuffer, 0, receiveBuffer.Length, 0, new AsyncCallback (ReceiveCallback), null);

					System.Threading.Thread.Sleep (1);
				}
			} catch (Exception e) {
				Debug.Log (e.ToString ());
			}
		}
	}

	private void ReceiveCallback(IAsyncResult AR) {
		int received = handler.EndReceive (AR);
		if (received <= 0) {
			return;
		}

		byte[] recData = new byte[received];
		Buffer.BlockCopy (receiveBuffer, 0, recData, 0, received);

		Message receivedMessage = deserializeMessage (recData);

		Debug.Log ("DATA RECEIVED: " + receivedMessage.commandID);

		//TODO process data here

		handler.BeginReceive (receiveBuffer, 0, receiveBuffer.Length, 0, new AsyncCallback(ReceiveCallback),null);
	}

	public string Send(Message data) {

		string response = "Operation timeout";

		if (handler != null) {

			SocketAsyncEventArgs socketEventArgs = new SocketAsyncEventArgs ();
			socketEventArgs.RemoteEndPoint = handler.RemoteEndPoint;
			socketEventArgs.UserToken = null;

			socketEventArgs.Completed += new EventHandler<SocketAsyncEventArgs> (delegate(object s, SocketAsyncEventArgs e) {
				response = e.SocketError.ToString ();
				_serverDone.Set ();
			});
				
			byte[] payload = serializeMessage (data);
			socketEventArgs.SetBuffer (payload, 0, payload.Length);

			_serverDone.Reset ();
			handler.SendAsync (socketEventArgs);

			_serverDone.WaitOne (TIMEOUT_MILLISECONDS);

		} else {
			response = "Socket not initialized";
		}
		return response;
	}


	private byte[] serializeMessage(Message m) {
		MemoryStream memoryStream = new MemoryStream ();
		DataContractSerializer serializer = new DataContractSerializer (typeof(Message));
		serializer.WriteObject (memoryStream, m);
		byte[] serializedData = memoryStream.ToArray ();
		memoryStream.Close ();
		return serializedData;
	}

	private Message deserializeMessage(byte[] data) {
		Message m = null;
		try {

			DataContractSerializer serializer = new DataContractSerializer (typeof(Message));
			MemoryStream memoryStream = new MemoryStream (data);
			m = serializer.ReadObject(memoryStream) as Message;
			memoryStream.Close();

		} catch(Exception e) {
			Debug.LogError(string.Format("Deserialization Error: {0}", e.Message));
		}

		return m;

	}


	void stopServer() {
		keepReading = false;
		if (socketThread != null) {
			socketThread.Abort ();
		}

		if (handler != null && handler.Connected) {
			handler.Disconnect (false);
			Debug.Log ("Disconnected");
		}
	}

	void OnDisable() {
		stopServer ();
	}

}
