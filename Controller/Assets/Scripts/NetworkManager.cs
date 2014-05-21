using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	public	static	NetworkManager	Instance;
	public	GameObject	characterPrefab;
	public	GameObject	character;
	
	void Awake()
	{
		if (Instance == null)
			Instance = this;
	}
	
//	public enum LoginState
//	{
//		Start,
//		Login,
//	}
//	public	LoginState		loginState = LoginState.Start;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void ConnectToServer (string IPAddress) {
		Debug.Log("Connecting : " + IPAddress);
		PlayerPrefs.SetString("IPAddress", IPAddress);
		//		Network.useNat = false;
		Network.Connect(IPAddress, 5000);
	}
	
	void OnConnectedToServer () {
		Debug.Log("Client Connected");
		GUIManager.Instance.showLoginGUI = false;
		GUIManager.Instance.disconnected = false;
		ControllerManager.Instance.controllerState = ControllerManager.ControllerState.Begin;
		AddtoServer();
	}
	
	void AddtoServer () {
		NetworkViewID viewID = Network.AllocateViewID();
		networkView.RPC("SetupPlayer", RPCMode.Server, viewID);
		SetupPlayer(viewID);
	}

	[RPC]
	void SetupPlayer (NetworkViewID viewID) {
		if(character == null) {
			character = Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		}
		character.networkView.viewID = viewID;
	}
	
	void OnFailedToConnect(NetworkConnectionError error) {
		Debug.Log("Could not connect to server: "+ error);
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		if (info == NetworkDisconnection.LostConnection)
			Debug.Log("Lost connection to the server");
		else
			Debug.Log("Successfully diconnected from the server");

		GUIManager.Instance.disconnected = true;
	}
}

