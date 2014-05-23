using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	public	float	Acceleration = 1.5f;

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ball")
		{
			Vector2 relativeDirection = (collision.gameObject.transform.position - transform.position).normalized;
			collision.rigidbody.AddForce(relativeDirection * Acceleration);
		}
	}
}
