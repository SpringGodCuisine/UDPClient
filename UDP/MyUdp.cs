using UnityEngine;
using System;
using System.Text;
using System.Threading;

using System.Net;
using System.Net.Sockets;
 
public class MyUdp {  

	private UdpClient sendClient = null;
//	private IPEndPoint sendEndPort;
	private bool isRun;

	private bool isRecv;
	public void StartClientUdp(string _ip){

//		if (sendEndPort != null) {
//			Debug.Log ("客户端udp已经启动~");
//			return;
//		}
			
		if (isRun) {
			Debug.Log ("客户端udp已经启动~");
			return;
		}
		isRun = true;

		sendClient = UdpManager.Instance.GetClient();
//		sendEndPort = new IPEndPoint(IPAddress.Parse(_ip), NetConfig.UdpSendPort);

	//	StartRecvMessage ();
	}
		
	private void StartRecvMessage(){
		Thread t = new Thread(new ThreadStart(RecvThread));
		t.Start();
	}

	public void StopRecvMessage(){
		isRecv = false;
	}

	public void EndClientUdp(){
		try {
			isRun = false;
			isRecv = false;
//			if (sendEndPort != null) {
//				UdpManager.Instance.CloseUdpClient();
//				sendClient = null;
//				sendEndPort = null;
//			}
			UdpManager.Instance.CloseUdpClient();
			sendClient = null; 	
		} catch (Exception ex) {
			Debug.Log ("udp连接关闭异常:" + ex.Message);
		}

	}

	public void SendMessage(byte[] _mes){
		if (isRun) {
			try {
				sendClient.Send(_mes,_mes.Length); 
			} catch (Exception ex) {
				Debug.Log ("udp发送失败:" + ex.Message);
			}
		}	}


	private void RecvThread()
	{
		isRecv = true;
		IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("192.169.1.18"), UdpManager.Instance.localPort);
		while (isRecv)
		{
			try {
				byte[] buf = sendClient.Receive(ref endpoint);

		 
//				Debug.Log("发送量:" + buf.Length.ToString() + "," + GameData.Instance().recvNum.ToString());
			} catch (Exception ex) {
				Debug.Log ("udpClient接收数据异常:" + ex.Message);
			}
		}
		Debug.Log ("udp接收线程退出~~~~~");
	}


	void OnDestroy()
	{
		EndClientUdp ();
	}
}