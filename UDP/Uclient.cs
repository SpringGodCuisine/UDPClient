using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uclient : MonoBehaviour {

	private MyUdp upd;
	// Use this for initialization
	private void Start () {
		StartClientUdp();
	}

	private void StartClientUdp()
	{
		 
		upd = new MyUdp();
		upd.StartClientUdp("192.168.1.18");
		const string data = "aaaabbbbcccc";
		 
		var message = System.Text.Encoding.UTF8.GetBytes(data);
        for (var i =0; i<20;i++)
        {
			upd.SendMessage(message);
		}

	}

}
