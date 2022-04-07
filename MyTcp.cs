using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets; 
using System.Threading; 

public class MyTcp
{
	private static MyTcp singleInstance;
	private static readonly object padlock = new object();

	private byte[] result = new byte[1024];
	private Socket clientSocket;

	public bool isRun = false;

	private Action<bool> ac_connect;
	public static MyTcp Instance
	{
		get
		{
			lock (padlock)  // 加锁保证单例唯一
			{
				if (singleInstance == null)
				{
					singleInstance = new MyTcp();
				}
				return singleInstance;
			}
		}
	}

	public void ConnectServer(string _ip, Action<bool> _result)
	{
		//设定服务器IP地址  
	    ac_connect = _result;
		IPAddress ip;
		var isRight = IPAddress.TryParse(_ip, out ip);

		if (!isRight)
		{
			Debug.Log("无效地址......" + _ip);
			_result(false);
			return;
		}
		clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		IPEndPoint endpoint = new IPEndPoint(ip, 13001);
		Debug.Log("开始连接tcp~");
		clientSocket.BeginConnect(endpoint, RequestConnectCallBack, clientSocket);
	}
	private void RequestConnectCallBack(IAsyncResult iar)
	{
		try
		{
			//还原原始的TcpClient对象
			Socket client = (Socket)iar.AsyncState; 
			client.EndConnect(iar);

			Debug.Log("连接服务器成功:" + client.RemoteEndPoint.ToString());
			isRun = true;
			ac_connect(true);
	 
		}
		catch (Exception e)
		{
			ac_connect(false);
	 

			Debug.Log("tcp连接异常:" + e.Message);
		}
		finally
		{

		}
	}
	 

	public void SendMessage(byte[] _mes)
	{
		if (isRun)
		{
			try
			{
				clientSocket.Send(_mes);
			}
			catch (Exception ex)
			{
		 	    EndClient();
				Debug.Log("发送数据异常:" + ex.Message);
			}
		}
	}

	public void EndClient()
	{

		isRun = false;

		if (clientSocket != null)
		{
			try
			{
				clientSocket.Close();
				clientSocket = null;
				Debug.Log("关闭tcp连接");
			}
			catch (Exception ex)
			{
				Debug.Log("关闭tcp连接异常111:" + ex.Message);
			}
		}
	}
 
	 

}
