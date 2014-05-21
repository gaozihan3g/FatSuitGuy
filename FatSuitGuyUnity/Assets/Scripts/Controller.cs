using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public Vector3 direction;
	public float sendRate = 15f;
	private float timer = 0;

	void OnGUI()
	{
		if (Network.isClient)
		{
//			if (GUI.Button(new Rect(100, 100, 100, 100), "Say Hello"))
//				SayHello();
			GUI.Label(new Rect(100, 100, 200, 200), "The controller is working!" + direction);
		}
	}

	void SendInput()
	{
		direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
		networkView.RPC("GetDirection", RPCMode.Server, direction);
	}

	void Update()
	{
		if (!Network.isClient)
			return;
		timer += Time.deltaTime;
		if (timer >= 1f / sendRate)
		{
			timer = 0f;
			SendInput();
		}

	}


	[RPC]
	void GetDirection(Vector3 dir)
	{
	}
}
