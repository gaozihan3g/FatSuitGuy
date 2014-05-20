using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Character c = other.gameObject.GetComponent<Character>();

			//player play death animation

			//respawn player
			GameManager.Instance.RespawnPlayer(c.playerID);

			Destroy(other.gameObject);
		}
	}
}
