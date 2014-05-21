using UnityEngine;
using System.Collections;

public class PlayerSpawnPoint : MonoBehaviour {
	
	public GameObject playerPrefab;
	public int id;
//	public bool isControlled;
	public float respawnDelay = 3;

	void Start()
	{
//		SpawnPlayer();
	}

	public void SpawnPlayer(NetworkViewID viewID)
	{
		GameObject go = Instantiate(playerPrefab, transform.position, Quaternion.identity) as GameObject;
		go.name += "_" + id;
		go.GetComponent<NetworkView>().viewID = viewID;
		Character c = go.GetComponent<Character>();
//		c.isControlled = isControlled;
		c.playerID = id;
		GameManager.Instance.players[id] = c;
	}

	public void RespawnPlayer(NetworkViewID viewID)
	{
		StartCoroutine(_RespawnPlayer(viewID));
	}

	IEnumerator _RespawnPlayer(NetworkViewID viewID)
	{
		yield return new WaitForSeconds(respawnDelay);
		SpawnPlayer(viewID);
	}

}
