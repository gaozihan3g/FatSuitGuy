using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour {
	public	static		CharacterControl	Instance;
	
	void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SendPos (Vector3 pos) {
		networkView.RPC("ChangeDirection", RPCMode.Server, pos);
	}

	[RPC]
	void ChangeDirection (Vector3 pos) {

	}

	public void SendModeName (string modeName) {
		networkView.RPC("ChangeMode", RPCMode.Server, modeName);
	}
	
	[RPC]
	void ChangeMode (string modeName) {
		
	}
}
