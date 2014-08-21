using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;

public class OscServer : MonoBehaviour
{
	public int listenPort = 6666;
	public float test = 1.0f;
	public GameObject player1;
	public GameObject player2;

	UdpClient udpClient;
	IPEndPoint endPoint;
	Osc.Parser osc = new Osc.Parser ();
	private String player1Id = "none";
	private String player2Id = "none";
	
	void Start ()
	{
		endPoint = new IPEndPoint (IPAddress.Any, listenPort);
		udpClient = new UdpClient (endPoint);

	}

	void createPlayer(String id){
		if(player1Id == "none"){
			//make player1
			player1Id = id;
			setPlayerToStage(1);
			return;
		}
		if(player2Id == "none"){
			//make player2
			player2Id = id;
			setPlayerToStage(2);
			return;
		}

		return;
	}

	void setPlayerToStage(int pid){
		switch(pid){
			case 1:
				var p1 = Instantiate(player1);
				p1.name = "p1";
				break;
			case 2:
				Instantiate(player2);
				p1.name = "p2";
				break;
		}
		Debug.Log ("set: " + pid);
		return;
	}

	void removePlayerFromStage(int pid){
		switch(pid){
			case 1:
				var target = GameObject.Find ("/p1");
				if(target){
					target.SendMessage ("RemoveOwn");
				}
				break;
			case 2:
				Destroy(GameObject.Find ("/p2"));
				if(target){
					target.SendMessage ("RemoveOwn");
				}
				break;
		}
		Debug.Log ("remove: " + pid);
		return;
	}

	void deletePlayer(String id){
		if(player1Id == id){
			//delete player1
			removePlayerFromStage(1);
			player1Id = "none";
			return;
		}

		if(player2Id == id){
			//delete player2
			removePlayerFromStage(2);
			player2Id = "none";
			return;
		}
	}
	void SwitchMethod(String pid,int paramator){
		//
		switch (paramator){
		case 3:
			createPlayer(pid);
			break;
		}
	}
	void Update ()
	{
		while (udpClient.Available > 0) {
			osc.FeedData (udpClient.Receive (ref endPoint));
		}
		
		while (osc.MessageCount > 0) {
			var msg = osc.PopMessage ();

//			var target = GameObject.Find (msg.path.Replace ("/", "_"));
//			if (target) {
//				target.SendMessage ("OnOscMessage", msg.data [0]);
//			}
//			SwitchMethod(msg.path,msg.data[0]);
			Debug.Log(msg.path+msg.data[0].GetType());
		}
	}
}

