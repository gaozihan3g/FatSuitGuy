using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	public PlayerSpawnPoint[] playerSpawnPoints;
	public Character[] players;
	public GameObject[] playerPrefabs;

	public float totalTime;
	private float timer;

	public int[] scores;
	public int numOfPlayers = 0;
	public	int	currentNumOfPlayers;
	public	GameObject	wallsPrefab;
	public	GameObject	ballPrefab;
	public	GameObject	doorsPrefab;


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
	
	void CreatePropsAddHP()
	{
		PowerUpFactory.Instance.GenerateAddHP();
	}

	public void RespawnPlayer(int id)
	{
		playerSpawnPoints[id].RespawnPlayer();
	}

	public void SpawnPlayer()
	{

	}
	
	public void CheckWin () {
		if(currentNumOfPlayers == 1) {
			for(int i = 0; i < numOfPlayers; i ++) {
				if(players[i].GetComponent<Character>().playerCurHP > 0) {
					GUIManager.Instance.winPlayerID = i + 1;
				}
			}
			GUIManager.Instance.guiState = GUIManager.GUIState.End;
		}
	}

	public void GameStart()
	{
		InvokeRepeating("CreateProps", 1f, 2f);
		InvokeRepeating("CreatePropsAddHP", 10f, 20f);
		GUIManager.Instance.guiState = GUIManager.GUIState.Game;
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

		GameObject obj = playerPrefabs[Random.Range(0, playerPrefabs.Length)];
		playerSpawnPoints[numOfPlayers].SpawnPlayer(obj, viewID);

		numOfPlayers++;
		currentNumOfPlayers = numOfPlayers;
	}

	public void ChangeModeToFoodball () {
		GameObject walls = Instantiate(wallsPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		GameObject ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		GameObject doors = Instantiate(doorsPrefab, Vector3.zero, Quaternion.identity) as GameObject;

	}

}
