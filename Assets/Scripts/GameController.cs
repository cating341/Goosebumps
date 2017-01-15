using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public struct SoldierPosition {
	public Vector3 instPos;
	public Vector2 bound;
	public SoldierPosition(Vector3 instPos, Vector2 bound) {
		this.instPos = instPos;
		this.bound = bound;
	}
}

public class GameController : MonoBehaviour {

    public GameObject timerText;
    public GameObject totalTimerText; 
	public GameObject risingText;
    public GameObject gameoverCanvas;
    public GameObject nextChapBtn;
    public GameObject[] bakingLadders;
	public GameObject panel;
    public GameObject namePanel;
    public Text nameInputText;
	public Text Rank;
	public Text Name;
	public Text Score;
	public Text myRank;
	public Text myName;
	public Text myScore;
	public Text ChapterText;
	public Text DifficultyText;
	public GameObject kingMonster;
	public float kingMonsterAppear;
	public GameObject soldierMonster;

    string playerName;
	LeaderBoardController leaderBoardController;
	MySceneManager sceneManager;

    float MAXTIMER = 1;
    float timer = 0;
    float currentTimer = 0;
    bool gameOver = false;
	bool canBeDestroyed = false;
	List<KeyValuePair<GameObject, Vector3>> rebornList = new List<KeyValuePair<GameObject, Vector3>>();
    GameObject player;

    // Use this for initialization
    void Start () {
		sceneManager = GameObject.Find ("SceneManager").GetComponent<MySceneManager> ();
		leaderBoardController = GetComponent<LeaderBoardController> ();
        gameoverCanvas.SetActive(false);
        gameOver = false;
        nextChapBtn.SetActive(false);
		InstantiateMonsters ();
	}
	
	// Update is called once per frame
	void Update () {
        
            timer += Time.deltaTime;
            if(!gameOver)
                currentTimer += Time.deltaTime;

            if (timer > 1.0f )
            {
                timerText.GetComponent<Text>().text = ((int)currentTimer).ToString() + " sec";
                timer = 0;
            }

		if (Input.GetKeyDown (KeyCode.Escape)) {
			PauseGame ();
		} else if (UserControl.GetGearsBack) {
			// Remove gears
			sceneManager.removeAllFromGearList();
		}

	}

	private void InstantiateMonsters() {
		Invoke ("InstantiateKingMonster", kingMonsterAppear);
		SoldierPosition[] soldier1Pos = new SoldierPosition[] {
			new SoldierPosition(new Vector3(-7.262046f, -3.47f, -3.735428f), new Vector2(-13f, 13f)),
			new SoldierPosition(new Vector3(-0.23f, 0.21f, -3.735428f), new Vector2(-13f, 13f)),
			new SoldierPosition(new Vector3(5.01f, 4.63f, -3.96f), new Vector2(-13f, 13f))
		};
		SoldierPosition[] soldier2Pos = new SoldierPosition[] {
			new SoldierPosition (new Vector3 (-13.09f, -0.64f, -4.01f), new Vector2 (-13f, 10f)), 
			new SoldierPosition (new Vector3 (-2.56f, 3.68f, -4.01f), new Vector2 (-13f, 12f)),
			new SoldierPosition (new Vector3 (-6.7f, 8.12f, -4.01f), new Vector2 (-13f, 0f))
		};
		int level = sceneManager.GetLevel();
		int difficulty = sceneManager.getDifficulty();
		for (int i = 0; i < difficulty + 1; i++) {
			GameObject newMonster = new GameObject();
			if (level == 1) {
				newMonster = (GameObject)Instantiate (soldierMonster, soldier1Pos [i].instPos, soldierMonster.transform.rotation);
				newMonster.GetComponent<AIInformation> ().floor = i + 1;
				newMonster.GetComponent<SoldierProperties> ().leftBound = soldier1Pos [i].bound.x;
				newMonster.GetComponent<SoldierProperties> ().rightBound = soldier1Pos [i].bound.y;
				newMonster.GetComponent<SoldierProperties> ().heading = newMonster.GetComponent<SoldierProperties> ().rightBound;
				print (newMonster.GetComponent<SoldierProperties> ().leftBound);
//				print (newMonster.GetComponent<SoldierProperties> ().leftBound);
			} else if (level == 2) {
				newMonster = (GameObject) Instantiate (soldierMonster, soldier2Pos [i].instPos, soldierMonster.transform.rotation);
				newMonster.GetComponent<AIInformation> ().floor = i + 1;
				newMonster.GetComponent<SoldierProperties> ().leftBound = soldier2Pos [i].bound.x;
				newMonster.GetComponent<SoldierProperties> ().rightBound = soldier2Pos [i].bound.y;
				newMonster.GetComponent<SoldierProperties> ().heading = newMonster.GetComponent<SoldierProperties> ().rightBound;
			}
		}
		Physics.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("KingMonster"), false);
		Physics.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("SoldierMonster"), false);
	}

	void PauseGame(){
		Time.timeScale = (Time.timeScale == 0)? 1: 0;
	}

	// disable monster
	public void disableMonster(){
		GameObject.Find ("Player").GetComponent<Character> ().DisablePlayerMove ();
		GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
		foreach(GameObject m in monsters){
			//Debug.Log (m.name);
			m.GetComponent<BasicProperties>().NewDisability("faint", true);
			m.GetComponent<BasicProperties> ().Pause ();
		}

		Invoke ("enableMonster", 5.0f);
	}

	void enableMonster(){
		GameObject.Find ("Player").GetComponent<Character> ().EnablePlayerMove ();
		GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
		foreach(GameObject m in monsters){
			m.GetComponent<BasicProperties>().NewDisability("faint", false);
			m.GetComponent<BasicProperties> ().Resume ();
		}
	}

	public void AddPoint(float pt) {
		this.currentTimer += pt;

		GameObject risingScore = (GameObject)Instantiate (risingText, new Vector3 (-12.62742f, 4.577878f, -18.89f), Quaternion.identity);

		risingScore.transform.SetParent (panel.transform);
		risingScore.transform.localPosition = new Vector3 (-12.62742f, 4.577878f, -18.89f);
		risingScore.transform.localScale = new Vector3 (1, 1, 1);
		risingScore.GetComponent<RisingText> ().setup ((int)pt, 0.5f, 1);


	}

    public void GameOver()
    {
		if (!gameOver) {
            player = GameObject.FindWithTag("Player");
            player.GetComponent<Character>().DisablePlayerMove();
            gameOver = true;
			gameoverCanvas.SetActive (true);
			namePanel.SetActive (true);
			totalTimerText.GetComponent<Text> ().text = ((int)currentTimer).ToString ();
			ChapterText.GetComponent<Text> ().text = "Chapter" + sceneManager.GetLevel ().ToString ();
			DifficultyText.GetComponent<Text> ().text = sceneManager.getDifficultyStr ().ToUpper();
			if (currentTimer > MAXTIMER)
				nextChapBtn.SetActive (true);
		}
            
    }

    public void GotoNextChap()
    {
        sceneManager.removeAllFromSceceList();
		sceneManager.removeAllFromGearList ();
        if (sceneManager.currentSceneName == sceneManager.GAMESCENE1)
            Application.LoadLevel(sceneManager.TRANS1);
        if (sceneManager.currentSceneName == sceneManager.GAMESCENE2)
            Application.LoadLevel(sceneManager.GAMESCENE3);
    }

    public void GameRestart()
    {
        sceneManager.removeAllFromSceceList();
		if(sceneManager.currentSceneName == sceneManager.GAMESCENE1 )
			Application.LoadLevel(sceneManager.PREVIEW1);
		else if(sceneManager.currentSceneName == sceneManager.GAMESCENE2 )
			Application.LoadLevel(sceneManager.PREVIEW2);

    }

	public void BackToStartScene(){
		sceneManager.removeAllFromSceceList();
		Application.LoadLevel (sceneManager.START);
	}

    public void Exit()
    {
        Application.Quit();
    }

    public void SendScoreBtnClick()
    {
        playerName = nameInputText.text;
        if (playerName.Equals(""))
            playerName = "Player";
        namePanel.SetActive(false);
		int level = sceneManager.GetLevel ();
		int difficulty = sceneManager.getDifficulty ();
		StartCoroutine (leaderBoardController.SendScore ((int)currentTimer, playerName, level, difficulty, (lb) => {
			SetLeaderBoard(lb);
		}));
    }

	private void SetLeaderBoard(LeaderBoard lb) {

		myRank.text = lb.myRate.ToString ();
		int SHOW_RANK_NUM = 5;
		if (lb.leaderboard.Count < 5) {
			SHOW_RANK_NUM = lb.leaderboard.Count;
		}
		for (int i = 0; i < SHOW_RANK_NUM; i++) {
			playerInfo playerInfo = lb.leaderboard [i];
			Name.text += playerInfo.name + "\n";
			Score.text += playerInfo.score.ToString () + "\n";
//			Debug.Log (playerInfo.name + ": " + playerInfo.score);
		}
		myName.text = playerName + "\n";
		myScore.text = (int)currentTimer + "\n";
	}


    public void SetTheLadders(int count, GameObject g)
    { 
        bakingLadders[count].transform.position = g.transform.position;
        bakingLadders[count].SetActive(true); 
    }

    public void DisableLadders() {
        foreach (GameObject ladder in bakingLadders)
        {
            ladder.SetActive(false);
        }
    }

	private void InstantiateKingMonster() {
		GameObject obj = (GameObject) Instantiate (kingMonster);
		int level = sceneManager.GetLevel();
		if (canBeDestroyed)
			obj.GetComponent<PotionDamage> ().setStartToDestroyed ();

		if (level == 2)
			obj.GetComponent<PotionDamage> ().setHealth (sceneManager.getDifficulty () + 1);

	}

	public void setKingMonsterDestroy(){
		canBeDestroyed = true;
	}

	public void ReBornMonster(GameObject monster){
		GameObject newObj;
		Vector3 newPosition ;
		int level = sceneManager.GetLevel();

		if (monster.layer == LayerMask.NameToLayer ("KingMonster")) {
			newObj = kingMonster;
		} else {
			newObj = soldierMonster;
		}

		if (level == 1)
			newPosition = new Vector3 (-7.262046f, -3.47f, -3.735428f);
		else //if (level == 2)
			newPosition = monster.transform.position;
		
		rebornList.Add (new KeyValuePair<GameObject, Vector3> (newObj, newPosition));
		Invoke ("ExecuteReBorn", 15.0f);
	}

	void ExecuteReBorn(){
		KeyValuePair<GameObject, Vector3> item = rebornList [0];
		GameObject bornObj = (GameObject) Instantiate (item.Key, item.Value, Quaternion.identity);
        if (canBeDestroyed && bornObj.GetComponent<PotionDamage>())
        {
            bornObj.GetComponent<PotionDamage>().setHealth(sceneManager.getDifficulty() + 1);
            bornObj.GetComponent<PotionDamage>().setStartToDestroyed();
        }
		
		rebornList.RemoveAt (0);
	}

    public void enableNightmare(bool isNightmare)
    {
        if (isNightmare)
        {
            GameObject.Find("Player").GetComponentInChildren<Light>().enabled = true;
            GameObject.Find("Directional light").GetComponent<Light>().enabled = false;
        }
        else
        {
            GameObject.Find("Player").GetComponentInChildren<Light>().enabled = false;
            GameObject.Find("Directional light").GetComponent<Light>().enabled = true;
        }
    }
}
