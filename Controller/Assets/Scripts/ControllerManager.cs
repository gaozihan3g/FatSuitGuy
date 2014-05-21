using UnityEngine;
using System.Collections;

public class ControllerManager : MonoBehaviour {
	public	static		ControllerManager	Instance;
	public	float		joystickMaxDistance = 30;
	public	Texture2D	padTexture;
	public	Texture2D	joystickTexture;
	public	Vector2		padPos;
	public	Vector2		joystickPos;
	public	float		padSize;
	public	float		joystickSize;
	public	bool		showController;
	public	Vector2		lastJoystickPos;
	public	float		sendMessageDistance = 1;

	public enum ControllerState
	{
		Login,
		Begin,
		End,
	}
	public	ControllerState		controllerState = ControllerState.Login;
	
	void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	// Use this for initialization
	void Start () {
		padSize = Screen.width * 0.2f;
		joystickSize = Screen.height * 0.1f;
		joystickMaxDistance = padSize * 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		CheckState ();
	}

	void CheckState () {
		switch(controllerState) {
		case ControllerState.Login:
			break;
		case ControllerState.Begin:
			CheckFingerPosition ();
			break;
		case ControllerState.End:
			break;
		}
	}

	void CheckFingerPosition () {
		if(Input.GetMouseButtonDown (0)) {
			showController = true;
			padPos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
			joystickPos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
			lastJoystickPos = joystickPos;
		}
		if(Input.GetMouseButton (0)) {
			float dis = Vector2.Distance(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y), padPos);
			Vector2 mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
			if(dis < joystickMaxDistance) {
				joystickPos = mousePos;
			}else {
				float p = joystickMaxDistance / dis;
				float jx = p * (mousePos.x - padPos.x);
				float jy = p * (mousePos.y - padPos.y);
				joystickPos = padPos + new Vector2 (jx, jy);
			}
			if(Vector2.Distance(lastJoystickPos, joystickPos) > sendMessageDistance) {
				Vector3 relativePos = joystickPos - padPos;
				float factor = 1;
				if(dis < joystickMaxDistance) {
					factor = dis / joystickMaxDistance;
				}
				Vector3 pos = new Vector3(relativePos.x, -relativePos.y, 0).normalized * factor;
//				Debug.Log (pos);
				CharacterControl.Instance.SendPos(pos);
			}
		}
		if(Input.GetMouseButtonUp (0)) {
			joystickPos = padPos;
			CharacterControl.Instance.SendPos(new Vector3(0, 0, 0));
			showController = false;
		}
	}

	void OnGUI () {
		if(showController) {
			GUI.DrawTexture(new Rect(padPos.x - padSize * 0.5f, padPos.y - padSize * 0.5f, padSize, padSize), padTexture);
			GUI.DrawTexture(new Rect(joystickPos.x - joystickSize * 0.5f, joystickPos.y - joystickSize * 0.5f, joystickSize, joystickSize), joystickTexture);
		}
	}

}