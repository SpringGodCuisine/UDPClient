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

	void CreatUpd(){
		_udpClient = new UdpClient ();
		Debug.Log("CreatUpd   "  + localPort);
		IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("192.168.1.18"), 10011);
		_udpClient.Connect (endpoint);
		IPEndPoint _localEnd = (IPEndPoint)_udpClient.Client.LocalEndPoint;
		localPort = _localEnd.Port;
		Debug.Log ("udp参数:" + _localEnd.Address + "," + _localEnd.Port);
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
