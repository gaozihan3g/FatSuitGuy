using UnityEngine;
using System.Collections;

public class PlayerSpawnPoint : MonoBehaviour {
	
	public GameObject playerPrefab;
	public int id;
	public bool isControlled;
	public float respawnDelay = 3;

	void Start()
	{
		SpawnPlayer();
	}

	public void SpawnPlayer()
	{
		GameObject go = Instantiate(playerPrefab, transform.position, Quaternion.identity) as GameObject;
		go.name += "_" + id;
		Character c = go.GetComponent<Character>();
		c.isControlled = isControlled;
		c.playerID = id;
		GameManager.Instance.players[id] = c;
	}

	public void RespawnPlayer()
	{
		StartCoroutine(_RespawnPlayer());
	}

	IEnumerator _RespawnPlayer()
	{
		yield return new WaitForSeconds(respawnDelay);
		SpawnPlayer();
	}

}
