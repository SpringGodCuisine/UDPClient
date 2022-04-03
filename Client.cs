using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Client : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string _ip = "192.168.1.18";
		MyTcp.Instance.ConnectServer(_ip, (_result) => {
			if (_result)
			{
				Debug.Log("连接成功"); 


				string data = "aaaabbbbcccc";
				//byte[] message = System.Text.Encoding.UTF8.GetBytes(data);
				////MyTcp.Instance.SendMessage(message);
				//for (int i = 0; i < 100; i++)
				//{
				//	MyTcp.Instance.SendMessage(message);
				//}

				byte[] packheadByte = BitConverter.GetBytes((short)data.Length);
				byte[] message = System.Text.Encoding.UTF8.GetBytes(data);
				List<byte> sendMessage = new List<byte>();
				////包头信息
				sendMessage.AddRange(packheadByte);
				sendMessage.AddRange(message);

				 for (int i = 0; i < 100; i++)
				 {
					MyTcp.Instance.SendMessage(sendMessage.ToArray());
				 }


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
