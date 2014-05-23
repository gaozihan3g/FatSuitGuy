using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public	int	owner;
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Ball")
		{
			Debug.Log ("Goal???");
			if(owner == 0 && GameManager.Instance.players[1] != null) {
				GameManager.Instance.players[1].LoseHP();
			} else {
				GameManager.Instance.players[0].LoseHP();
			}
		}
	}
}
