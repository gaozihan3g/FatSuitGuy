using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	public	static	GUIManager	Instance;
	public	GUISkin		clientSkin;
	public	bool		showLoginGUI = true;
	private	float 		width;
	private	float 		height;
	private string		IPAddress = "127.0.0.1";//127.0.0.1   192.168.10.103
	public	bool		disconnected;


	void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	// Use this for initialization
	void Start () {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			IPAddress = "10.159.23.124";
		} else {
			IPAddress = "127.0.0.1";
		}
		width = Screen.width;
		height = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnGUI () {
		GUI.skin = clientSkin;
		if(showLoginGUI) {
			float btnW = Screen.width * 0.1f;
			float btnH = Screen.height * 0.1f;
			//Login: 
			GUI.Label(new Rect(width * 0.15f, height * 0.5f, width * 0.2f, height * 0.1f), "IP address : ", "LoginText");
			IPAddress = GUI.TextField(new Rect(width * 0.4f, height * 0.5f, width * 0.2f, height * 0.1f), IPAddress, "LoginText");
			GUI.TextField(new Rect(width * 0.4f, height * 0.5f, width * 0.2f, height * 0.1f), "");
			GUI.Label(new Rect(width * 0.65f, height * 0.5f, btnW, btnH), "Login", "LoginBtn");
			if(GUI.Button(new Rect(width * 0.65f, height * 0.5f, btnW, btnH), "")) {
				NetworkManager.Instance.ConnectToServer(IPAddress);
			}
		}
		
		if(disconnected) {
			GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Re-Login", "ReLoginBtn");
			if(GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "")) {
				NetworkManager.Instance.ConnectToServer(IPAddress);
			}
		}
	}
}
