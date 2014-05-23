using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	public static NetworkManager Instance;
	public string ipAddress = "127.0.0.1";
	public int port = 5000;
	public GameObject controllerPrefab;
	
	void Awake()
	{
		Instance = this;
	}

	void StartServer()
	{
		Network.InitializeServer(4, port, !Network.HavePublicAddress());
	}

	void OnServerInitialized()
	{
		Debug.Log("Server Initialized");
		GameManager.Instance.GameStart();
	}

	void JoinServer()
	{
		Network.Connect(ipAddress, port);
	}

	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
		// do sth here
		NetworkViewID id = Network.AllocateViewID();
		networkView.RPC("SetupPlayer", RPCMode.Server, id);
		SetupController(id);
    }

	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 100, 100), "Start Server"))
				StartServer();
			
			ipAddress = GUI.TextField (new Rect(300, 100, 200, 20), ipAddress, 25);
			
			if (GUI.Button(new Rect(100, 300, 100, 100), "Join Server"))
				JoinServer();
		}
	}

	void SetupController(NetworkViewID id)
	{
		GameObject go = Instantiate(controllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		go.GetComponent<NetworkView>().viewID = id;
	}
	
	[RPC]
	void SetupPlayer(NetworkViewID id)
	{
		print ("SetupPlayer from NetworkManager");
		GameManager.Instance.SetupPlayer(id);
    }

	public void SendModeName(string modeName) {
		networkView.RPC("ChangeMode", RPCMode.Others, modeName);
	}

	[RPC]
	void ChangeMode (string modeName) {

	}
}
