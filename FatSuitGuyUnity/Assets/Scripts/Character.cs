﻿using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public int						playerID;
	public float					maxSpeed;
	public float					bumpAcceleration;
	public Vector2					direction;

	public Vector2					initPosition;
	public bool						canBeControlled = true;
	public float					playerMaxHP = 10;
	public float					playerCurHP = 10;
	public Texture2D				avatar;

	public float					minSize = 1f;
	public float					maxSize = 2f;

	public float					sizeIncreaseAmount = 0.2f;
	public float					sizeDecreaseAmount = 0.1f;

	public float					massIncreaseAmount = 0.1f;
	public float					massDecreaseAmount = 0.05f;

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
		playerCurHP = playerMaxHP;
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
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ball")
		{
			GameObject other = collision.gameObject;
			Vector2 relativeDirection = (other.transform.position - transform.position).normalized;
			other.rigidbody2D.AddForce(relativeDirection * Mass * bumpAcceleration);
			other.SendMessage("Drop");
		}
	}

	void Grow()
	{
		if (Size >= maxSize)
			return;
		Size += sizeIncreaseAmount;
		Mass += massIncreaseAmount;
	}

	void Drop()
	{
		if (Size <= minSize)
			return;
		Size -= sizeDecreaseAmount;
		Mass -= massDecreaseAmount;
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
		LoseHP();
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

	public void GetDirection(Vector3 dir)
	{
		Direction = dir;
	}

}
