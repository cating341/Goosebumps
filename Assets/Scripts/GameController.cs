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
	public GameObject kingMonster;
	public float kingMonsterAppear;
	public GameObject soldierMonster;

    string playerName;
	LeaderBoardController leaderBoardController;

    float MAXTIMER = 1;
    float timer = 0;
    float currentTimer = 0;
    bool gameOver = false;
	bool canBeDestroyed = false;
	List<GameObject> rebornList = new List<GameObject>();

	// Use this for initialization
	void Start () {
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
		}
        
	}

	private void InstantiateMonsters() {
		Invoke ("InstantiateKingMonster", kingMonsterAppear);
		SoldierPosition[] soldier1Pos = new SoldierPosition[] {
			new SoldierPosition(new Vector3(-7.262046f, -3.47f, -3.735428f), new Vector2(-10.8f, 10.8f)),
			new SoldierPosition(new Vector3(-0.23f, 0.21f, -3.735428f), new Vector2(-10.8f, 10.8f)),
			new SoldierPosition(new Vector3(5.01f, 4.63f, -3.96f), new Vector2(-10.8f, 10.8f))
		};
		SoldierPosition[] soldier2Pos = new SoldierPosition[] {
			new SoldierPosition (new Vector3 (-13.09f, -0.64f, -5.01f), new Vector2 (-13f, 10f)), 
			new SoldierPosition (new Vector3 (-2.56f, 3.68f, -5.01f), new Vector2 (-13f, 13f)),
			new SoldierPosition (new Vector3 (-6.7f, 8.12f, -5.01f), new Vector2 (-13f, 0f))
		};
		int level = GameObject.Find("SceneManager").GetComponent<MySceneManager>().GetLevel();
		int difficulty = GameObject.Find("SceneManager").GetComponent<MySceneManager>().getDifficulty();
		for (int i = 0; i < difficulty + 1; i++) {
			GameObject newMonster = new GameObject();
			if (level == 1) {
				newMonster = (GameObject)Instantiate (soldierMonster, soldier1Pos [i].instPos, soldierMonster.transform.rotation);
				newMonster.GetComponent<AIInformation> ().floor = i + 1;
				newMonster.GetComponent<SoldierProperties> ().leftBound = soldier1Pos [i].bound.x;
				newMonster.GetComponent<SoldierProperties> ().rightBound = soldier1Pos [i].bound.y;
			} else if (level == 2) {
				newMonster = (GameObject) Instantiate (soldierMonster, soldier2Pos [i].instPos, soldierMonster.transform.rotation);
				newMonster.GetComponent<AIInformation> ().floor = i + 1;
				newMonster.GetComponent<SoldierProperties> ().leftBound = soldier2Pos [i].bound.x;
				newMonster.GetComponent<SoldierProperties> ().rightBound = soldier2Pos [i].bound.y;
			}
		}
	}

	void PauseGame(){
		Time.timeScale = (Time.timeScale == 0)? 1: 0;
	}

	// disable monster
	public void disableMonster(){
		GameObject.Find ("Player").GetComponent<Character> ().DisablePlayerMove ();
		GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
		foreach(GameObject m in monsters){
			m.GetComponent<BasicProperties> ().Pause ();
		}
		Invoke ("enableMonster", 5.0f);
	}

	void enableMonster(){
		GameObject.Find ("Player").GetComponent<Character> ().EnablePlayerMove ();
		GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
		foreach(GameObject m in monsters){
			m.GetComponent<BasicProperties> ().Resume ();
		}
	}

	public void AddPoint(float pt) {
		this.currentTimer += pt;

		GameObject risingScore = (GameObject)Instantiate (risingText, new Vector3 (-12.62742f, 4.577878f, -18.89f), Quaternion.identity);

		risingScore.transform.SetParent (panel.transform);
		risingScore.transform.localPosition = new Vector3 (-12.62742f, 4.577878f, -18.89f);
		risingScore.transform.localScale = new Vector3 (1, 1, 1);
		risingScore.GetComponent<RisingText> ().setup (20, 0.5f, 1);


	}

    public void GameOver()
    {
		if (!gameOver) {
			gameOver = true;
			gameoverCanvas.SetActive (true);
			namePanel.SetActive (true);
			totalTimerText.GetComponent<Text> ().text = ((int)currentTimer).ToString ();
			if (currentTimer > MAXTIMER)
				nextChapBtn.SetActive (true);
		}
            
    }

    public void GotoNextChap()
    {
        GameObject.Find("SceneManager").GetComponent<MySceneManager>().removeAllFromSceceList();
        if (GameObject.Find("SceneManager").GetComponent<MySceneManager>().currentSceneName == GameObject.Find("SceneManager").GetComponent<MySceneManager>().GAMESCENE1)
            Application.LoadLevel(GameObject.Find("SceneManager").GetComponent<MySceneManager>().TRANS1);
        if (GameObject.Find("SceneManager").GetComponent<MySceneManager>().currentSceneName == GameObject.Find("SceneManager").GetComponent<MySceneManager>().GAMESCENE2)
            Application.LoadLevel(GameObject.Find("SceneManager").GetComponent<MySceneManager>().GAMESCENE3);
    }

    public void GameRestart()
    {
        GameObject.Find("SceneManager").GetComponent<MySceneManager>().removeAllFromSceceList();
		if(GameObject.Find("SceneManager").GetComponent<MySceneManager>().currentSceneName == GameObject.Find("SceneManager").GetComponent<MySceneManager>().GAMESCENE1 )
			Application.LoadLevel(GameObject.Find("SceneManager").GetComponent<MySceneManager>().PREVIEW1);
		else if(GameObject.Find("SceneManager").GetComponent<MySceneManager>().currentSceneName == GameObject.Find("SceneManager").GetComponent<MySceneManager>().GAMESCENE2 )
			Application.LoadLevel(GameObject.Find("SceneManager").GetComponent<MySceneManager>().PREVIEW2);

    }

	public void BackToStartScene(){
		GameObject.Find("SceneManager").GetComponent<MySceneManager>().removeAllFromSceceList();
		Application.LoadLevel (GameObject.Find ("SceneManager").GetComponent<MySceneManager> ().START);
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
		int level = GameObject.Find ("SceneManager").GetComponent<MySceneManager> ().GetLevel ();
		StartCoroutine (leaderBoardController.SendScore ((int)currentTimer, playerName, level, (lb) => {
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
		if (canBeDestroyed)
			obj.GetComponent<PotionDamage> ().setStartToDestroyed ();

	}

	public void setKingMonsterDestroy(){
		canBeDestroyed = true;
	}

	public void ReBornMonster(GameObject monster){
		GameObject newObj;
		if (monster.layer == LayerMask.NameToLayer ("KingMonster")) {
			newObj = kingMonster;
		} else {
			newObj = soldierMonster;
		}
		rebornList.Add (newObj);
		Invoke ("ExecuteReBorn", 15.0f);
	}

	void ExecuteReBorn(){
		GameObject obj = rebornList [0];
		Instantiate (obj, new Vector3(-7.262046f, -3.47f, -3.735428f), Quaternion.identity);
		rebornList.RemoveAt (0);
	}
}
