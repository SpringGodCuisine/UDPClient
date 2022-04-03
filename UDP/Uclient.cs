using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uclient : MonoBehaviour {

	private MyUdp _upd;
	// Use this for initialization
	void Start () {
		StartClientUdp();
	}

	public void StartClientUdp()
	{
		 
		_upd = new MyUdp();
		_upd.StartClientUdp("192.168.1.18");
		string data = "aaaabbbbcccc";
		 
		byte[] message = System.Text.Encoding.UTF8.GetBytes(data);
        for (int i =0; i<20;i++)
        {
			_upd.SendMessage(message);
		}

	}

}
