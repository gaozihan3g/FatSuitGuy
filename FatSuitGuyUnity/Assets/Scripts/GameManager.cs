using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	public PlayerSpawnPoint[] playerSpawnPoints;
	public Character[] players;


	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		InvokeRepeating("CreateProps", 1f, 2f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CreateProps()
	{
		PowerUpFactory.Instance.GenerateFood();
	}

	public void RespawnPlayer(int id)
	{
		playerSpawnPoints[id].RespawnPlayer();
	}

}
