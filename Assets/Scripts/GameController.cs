using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

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
	public GameObject kingMonster;
	public float kingMonsterAppear;

    string playerName;
	LeaderBoardController leaderBoardController;

    float MAXTIMER = 1;
    float timer = 0;
    float currentTimer = 0;
    bool gameOver = false;
	bool canBeDestroyed = false;

	// Use this for initialization
	void Start () {
		leaderBoardController = GetComponent<LeaderBoardController> ();
        gameoverCanvas.SetActive(false);
        gameOver = false;
        nextChapBtn.SetActive(false); 
		Invoke ("InstantiateKingMonster", kingMonsterAppear);
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

	void PauseGame(){
		Time.timeScale = (Time.timeScale == 0)? 1: 0;
	}

	// disable monster
	public void disableMonster(){
		GameObject.Find ("Player").GetComponent<Character> ().DisablePlayerMove ();
		Invoke ("enableMonster", 5.0f);
	}

	void enableMonster(){
		GameObject.Find ("Player").GetComponent<Character> ().EnablePlayerMove ();
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
		StartCoroutine (leaderBoardController.SendScore ((int)currentTimer, playerName, (lb) => {
			SetLeaderBoard(lb);
		}));
    }

	private void SetLeaderBoard(LeaderBoard lb) {
		Rank.text += "\n" + lb.myRate.ToString ();
		foreach (playerInfo playerInfo in lb.leaderboard) {
			Name.text += "\n" + playerInfo.name;
			Score.text += "\n" + playerInfo.score.ToString ();
			Debug.Log (playerInfo.name + ": " + playerInfo.score);
		}
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
}
