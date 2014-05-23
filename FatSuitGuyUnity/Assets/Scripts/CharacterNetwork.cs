using UnityEngine;
using System.Collections;

public class CharacterNetwork : MonoBehaviour {
	public	Character	character;

	// Use this for initialization
	void Start () {
		character = gameObject.GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	[RPC]
	public void ChangeDirection(Vector3 dir)
	{
		character.GetDirection(dir);
	}
	
	[RPC]
	void ChangeMode (string modeName) {
		if(modeName == "Football") {
			GameManager.Instance.ChangeModeToFoodball();
		}
		NetworkManager.Instance.SendModeName(modeName);
		GUIManager.Instance.guiState = GUIManager.GUIState.Game;
	}
}
