﻿using UnityEngine;
using System.Collections;

public class AddHP : MonoBehaviour {

	public float rotationSpeed = 1f;
	
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "Player")
		{
			other.gameObject.SendMessage("GetHP");
			PowerUpFactory.Instance.addHPNum--;
			Destroy(gameObject);
		}
	}

	void Update()
	{
		transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
	}

}
