using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Character c = other.gameObject.GetComponent<Character>();

			//reduce hp:
			c.LoseHP();

			//player play death animation

			//respawn player
			GameManager.Instance.RespawnPlayer(c.playerID);

			c.ChangeDirection(new Vector3(0, 0, 0));
			c.transform.position = new Vector3(1000, 1000, 1000);
			c.renderer.enabled = false;
			c.canBeControlled = false;
		}
	}
}
