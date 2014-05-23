using UnityEngine;
using System.Collections;

public class PlayerSpawnPoint : MonoBehaviour {

	public	GameObject	player;
	public int id;
//	public bool isControlled;
	public float respawnDelay = 3;

	void Start()
	{
//		SpawnPlayer();
	}

	public void SpawnPlayer(GameObject obj, NetworkViewID viewID)
	{
		GameObject go = Instantiate(obj, transform.position, Quaternion.identity) as GameObject;
		go.name += "_" + id;
		go.GetComponent<NetworkView>().viewID = viewID;
		Character c = go.GetComponent<Character>();
		c.playerID = id;
		c.initPosition = transform.position;
		GameManager.Instance.players[id] = c;
		GUIManager.Instance.AddCharacter(c);
		player = go;
	}

	public void RespawnPlayer()
	{
		StartCoroutine(_RespawnPlayer());
	}

	IEnumerator _RespawnPlayer()
	{
		yield return new WaitForSeconds(respawnDelay);
		ResetPlayer(player);
	}

	void ResetPlayer (GameObject player) {
		Debug.Log ("Back to work!!!!!!!!!");
		player.transform.position = transform.position;
		player.renderer.enabled = true;
		player.GetComponent<Character>().canBeControlled = true;
	}
	
}
