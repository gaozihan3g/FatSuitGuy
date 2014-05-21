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
		InvokeRepeating("CreateProps", 1f, 2f);
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
//		GameObject go = Instantiate(padPrefab, initPositions[numberOfPlayers], Quaternion.identity) as GameObject;
//		go.GetComponent<NetworkView>().viewID = id;
//		numberOfPlayers++;
//		print ("SetupPlayer!!!");
//		players[numOfPlayers].networkView.viewID = id;
//		numOfPlayers++;
		playerSpawnPoints[numOfPlayers].SpawnPlayer(viewID);
		numOfPlayers++;
	}

}
