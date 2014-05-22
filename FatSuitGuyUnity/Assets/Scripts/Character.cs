using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public int						playerID;
	
	public float					maxSpeed;
	public float					bumpAcceleration;
//	public float					explosionForce;
	public Vector2					direction;
	public float					sizeLimit = 2f;
	public Vector2					initPosition;
	public bool						canBeControlled = true;
	
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

	public float Size {
		get {
			return transform.localScale.x;
		}

		set {
			transform.localScale = new Vector3(value, value, value);
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
		if (!canBeControlled)
			return;
		//if zero, early return
		if (Direction == Vector2.zero)
			return;

		//move
		Move();

		//rotate
		Rotate();
	}

	void GetInput()
	{
		Direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Vector2 relativeDirection = (collision.gameObject.transform.position - transform.position).normalized;
//			rigidbody2D.AddForce(-relativeDirection * Mass * bumpAcceleration);
			collision.rigidbody.AddForce(relativeDirection * Mass * bumpAcceleration);
		}
	}

	void Grow()
	{
		if (Size >= sizeLimit)
			return;
		Size += .2f;
		Mass += 1f;
	}

	void Move()
	{
		transform.Translate(Vector3.up * Direction.magnitude * MaxSpeed * Time.deltaTime);
	}

	void Rotate()
	{
		transform.rotation = Quaternion.LookRotation(Vector3.forward, Direction);
	}
	
	void Reset()
	{
		StartCoroutine(_Reset());
	}

	IEnumerator _Reset()
	{
		canBeControlled = false;
		yield return new WaitForSeconds(3f);
		//reset position
		transform.position = initPosition;
		
		//reset size
		transform.localScale = new Vector3(1f, 1f, 1f);
		
		//reset mass
		Mass = 1f;

		canBeControlled = true;
		//TODO: any other things need to be reset?
	}



	[RPC]
	void ChangeDirection(Vector3 dir)
	{
		Direction = dir;
//		curMaxSpeed = Direction.magnitude / new Vector3(1, 1, 0).magnitude * maxSpeed;
		// not neccessary, changed in the move function
	}

}
