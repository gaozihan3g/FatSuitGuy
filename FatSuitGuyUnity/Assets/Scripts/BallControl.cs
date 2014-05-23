using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {
	public	float	Acceleration = 1.5f;
	
	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
//			Vector2 relativeDirection = (transform.position - collision.gameObject.transform.position).normalized;
//			gameObject.rigidbody.AddForce(relativeDirection * Acceleration);
		}
	}
}
