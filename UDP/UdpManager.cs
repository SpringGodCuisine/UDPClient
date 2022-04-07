using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;

public class UdpManager {
	private static UdpManager singleInstance;
	private static readonly object padlock = new object();

	public UdpClient _udpClient = null;

	public int localPort;
	public static UdpManager Instance
	{
		get
		{
			lock (padlock)
			{
				if (singleInstance==null)
				{
					singleInstance = new UdpManager();
				}
				return singleInstance;
			}
		}
	}

	private UdpManager()
	{
		CreatUpd ();
	}

	public void Creat(){
	
	}

	private void CreatUpd(){
		_udpClient = new UdpClient ();
		Debug.Log("CreatUpd   "  + localPort);
		var endpoint = new IPEndPoint(IPAddress.Parse("192.168.1.18"), 10011);
		_udpClient.Connect (endpoint);
		var localEnd = (IPEndPoint)_udpClient.Client.LocalEndPoint;
		localPort = localEnd.Port;
		Debug.Log ("udp参数:" + localEnd.Address + "," + localEnd.Port);
	}

	public void Destory(){

		CloseUdpClient ();
		singleInstance = null;
	}

	public void CloseUdpClient(){
		if (_udpClient != null) {
			_udpClient.Close ();
			_udpClient = null;
		}
	}

	public UdpClient GetClient(){
		if (_udpClient == null) {
			CreatUpd ();
		}

		return _udpClient;
	}
		
}
