using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {
	public static GUIManager Instance;
	public	int			characterNum;
	public	List<Texture2D>	avatars = new List<Texture2D>();
	public	Texture2D	HPBar;
	public	List<Character>	characters = new List<Character>();
	private	float		screenW;
	private	float		screenH;
	public	bool		isEnd;
	public	int			winPlayerID;
	
	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		screenW = Screen.width;
		screenH = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddCharacter (Character character) {
		characters.Add (character);
		characterNum ++;
	}

	void OnGUI () {
		float avatarSize = screenH * 0.03f;
		float hpW = screenW * 0.06f;
		for(int i = 0; i < characterNum; i ++) {
			GUI.DrawTexture(new Rect(0, avatarSize * i, avatarSize, avatarSize), characters[i].avatar);
			GUI.DrawTexture(new Rect(avatarSize * 1.1f, (avatarSize + screenH * 0.01f) * i, hpW * characters[i].playerCurHP, avatarSize), HPBar);
		}
		if(isEnd) {
			if(GUI.Button (new Rect(0, 0, screenW, screenH), "Player" + winPlayerID + " WIN!!!")) {
				for(int i = 0; i < characterNum; i ++) {
					characters[i].playerCurHP = characters[i].playerMaxHP;
					GameManager.Instance.currentNumOfPlayers = GameManager.Instance.numOfPlayers;
				}
				isEnd = false;
			}
		}
	}
}
