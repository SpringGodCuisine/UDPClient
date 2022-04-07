using System;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		const string ip = "192.168.3.151";
		MyTcp.Instance.ConnectServer(ip, (result) => {
			if (result)
			{
				Debug.Log("连接成功");
				
				const string data = "aaaabbbbcccc";
				// var message = System.Text.Encoding.UTF8.GetBytes(data);
				// //MyTcp.Instance.SendMessage(message);
				// for (var i = 0; i < 100; i++)
				// {
				// 	MyTcp.Instance.SendMessage(message);
				// }

				var packheadByte = BitConverter.GetBytes((short)data.Length);
				var message = System.Text.Encoding.UTF8.GetBytes(data);
				var sendMessage = new List<byte>();
				////包头信息
				sendMessage.AddRange(packheadByte);
				sendMessage.AddRange(message);
				
				MyTcp.Instance.SendMessage(sendMessage.ToArray());


				//byte[] packheadByte = BitConverter.GetBytes((short)data.Length);
				//Debug.Log(packheadByte);
				//byte[] message = System.Text.Encoding.UTF8.GetBytes(data);
				//List<byte> sendMessage = new List<byte>();
				////包头信息
				//sendMessage.AddRange(packheadByte);
				//sendMessage.AddRange(message);  
				////Array.Copy(message, packheadByte, message.Length);
				//for (int i = 0; i < 100; i++)
				//{
				//	MyTcp.Instance.SendMessage(sendMessage.ToArray());
				//}
			}
			else
			{
				Debug.Log("连接失败"); 
			}
		});
	}
	void OnApplicationQuit()
	{ 
		MyTcp.Instance.EndClient(); 
	}

}
