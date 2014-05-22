using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public int playerID;
	public float acceleration;
	public float maxSpeed;
	public float rotationSpeed;
	public Vector2 direction;
//	public bool isControlled;
	public float explosionForce;
	public float sizeLimit = 2f;
	public	float	curMaxSpeed;
	public	float	playerMaxHP = 10;
	public	float	playerCurHP = 10;
	public	Texture2D	avatar;
	public	bool	isMoveFixed;
	
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
			if (value >= sizeLimit)
				return;
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
		playerCurHP = playerMaxHP;
	}

	void Update()
	{
//		if (!isControlled)
//			return;

		//get input
//		GetInput();

		//if zero, early return
		if (Direction == Vector2.zero)
			return;
		//move
		transform.Translate(Vector3.up * curMaxSpeed * acceleration * Time.deltaTime);
//		rigidbody2D.AddForce(transform.up * acceleration * Time.deltaTime);
		//rotate
		transform.rotation = Quaternion.LookRotation(Vector3.forward, Direction);

	}

	void GetInput()
	{
		Direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			rigidbody2D.AddForce(-Direction.normalized * explosionForce);
			collision.rigidbody.AddForce(Direction.normalized * explosionForce);
		}
	}

	void Grow()
	{
		print("grow!");
		Size += .2f;
	}

	void GetHP() {
		if(playerCurHP > 0 && playerCurHP < 10) {
			playerCurHP ++;
		}
	}
	
	public void LoseHP () {
		playerCurHP --;
//		Debug.Log (playerCurHP);
		CheckDead();
	}
	
	void CheckDead () {
		if(playerCurHP <= 0) {
			GameManager.Instance.currentNumOfPlayers --;
			GameManager.Instance.CheckWin();
		}
	}

	[RPC]
	public void ChangeDirection(Vector3 dir)
	{
		if(!isMoveFixed) {
			Direction = dir;
			curMaxSpeed = Direction.magnitude / new Vector3(1, 1, 0).magnitude * maxSpeed;
		}
	}

}
