using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player")
			print("Ahhhhhhhhhhhhh!");
			other.gameObject.SendMessage("Reset");
	}

}
