using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	public PlayerSpawnPoint[] playerSpawnPoints;
	public Character[] players;

	public float totalTime;
	private float timer;

	public int[] scores;
	public int numOfPlayers = 0;


	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CreateProps()
	{
		PowerUpFactory.Instance.GenerateFood();
	}

	public void RespawnPlayer(int id, NetworkViewID viewID)
	{
		playerSpawnPoints[id].RespawnPlayer(viewID);
	}

	public void SpawnPlayer()
	{

	}

	public void GameStart()
	{
		InvokeRepeating("CreateProps", 1f, 2f);
	}

	void OnGUI()
	{
		int i = 0;
		foreach (int score in scores)
		{
			GUI.Label(new Rect(0, 20 * i, 100, 20), "player" + i + ": " + score);
			i++;
		}
	}
	
	public void SetupPlayer(NetworkViewID viewID)
	{
		print ("SetupPlayer from GameManager");

		playerSpawnPoints[numOfPlayers].SpawnPlayer(viewID);
		numOfPlayers++;
	}

}
