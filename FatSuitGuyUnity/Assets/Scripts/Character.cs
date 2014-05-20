using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	
	public float acceleration;
	public float maxSpeed;
	public float rotationSpeed;
	public Vector2 direction;
	public bool isControlled;
	
	public float MaxSpeed {
		get {
			return maxSpeed;
		}
		set {
			maxSpeed = value;
		}
	}

	public float Mass {
		get {
			return rigidbody2D.mass;
		}

		set {
			rigidbody2D.mass = value;
		}
	}

	public Vector2 Direction {
		get {
			return direction;
		}
		set {
			direction = value;
		}
	}

	void Start()
	{

	}

	void Update()
	{
		if (!isControlled)
			return;
		//get input
		Direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		//if zero, early return
		if (Direction == Vector2.zero)
			return;
		//move
		transform.Translate(Vector3.up * acceleration * Time.deltaTime);
		//rotate
		transform.rotation = Quaternion.LookRotation(Vector3.forward, Direction);

	}

	void FixedUpdate()
	{

	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		print("Collide!");
		rigidbody2D.AddForceAtPosition(-Direction * 100f, collision.contacts[0].point);
	}

}
