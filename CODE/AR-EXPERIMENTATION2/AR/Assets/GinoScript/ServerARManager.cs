using UnityEngine;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using UnityEngine.UI;
using Vuforia;





public class ServerARManager : MonoBehaviour
{	
	public GameObject mgsConnection;
	public GameObject statusConnectionPanel;

	private static string SERVERSTARTED = " >> Server Started";
	private static string IPADDRESS = "Ip address: ";
	private static string MSGCONNECTION = " >> Accept connection from client";
	private static string MSGCIPNOTFOUD = "Local IP Address Not Found!";

	private TcpListener tcp_Listener;

	private Socket socketClient;

	private Magnet magnet;
	private AugmentationRenderer augmentationRenderer;

	private bool flagIsActive;


	void Start ()
	{
		tcp_Listener = new TcpListener (8888);
		tcp_Listener.Start ();
		mgsConnection.GetComponent<Text> ().text = IPADDRESS + GetLocalIPAddress ();
		statusConnectionPanel.SetActive (false);
		Debug.Log (SERVERSTARTED);
		Debug.Log (IPADDRESS + GetLocalIPAddress ());
		Debug.Log (MSGCONNECTION);

		augmentationRenderer = FindObjectOfType<AugmentationRenderer> ();

		magnet = (Magnet)GameObject.FindWithTag ("Magnet").GetComponent<Magnet> ();


	}


	void Update ()
	{

		if (tcp_Listener.Pending () && socketClient == null) {
			socketClient = tcp_Listener.AcceptSocket ();
			statusConnectionPanel.SetActive (true);
			flagIsActive = true;
		} else if (socketClient != null) {

			if (socketClient.Available>0) {
				byte[] tempbuffer = new byte[1000];
				socketClient.Receive (tempbuffer); // received byte array from client

				string str = System.Text.Encoding.Default.GetString (tempbuffer);

				int intCase = int.Parse (str);


				switch (intCase) {

				case 1:
					augmentationRenderer.handlerRenderOffOrOnfHands (3);
					break;
				
				case 2:
					augmentationRenderer.handlerRenderOffOrOnAll (3);
					break;
				case 3:
					magnet.nextScene ();
					break;


				}

				Debug.Log (str);
			}

		}


	}

	public static string GetLocalIPAddress()
	{
		var host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (var ip in host.AddressList)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork)
			{
				return ip.ToString();
			}
		}

		return MSGCIPNOTFOUD;
	}

	public bool isActive(){
		return flagIsActive;
	}


	}
	