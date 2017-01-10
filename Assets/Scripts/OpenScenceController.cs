using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpenScenceController : MonoBehaviour {

	public GameObject SelectScene;
	public GameObject StartScene;
	public GameObject RankCanvas;
	public GameObject[] gameview;
	public Text Score;
	public Text Rank;
	public Text Name;

	LeaderBoardController leaderboardController;

	int maxSceneCount = 2;
	int currentSelect = 0;

	int rankingChap = 0;
	int rankingDifficulty = 0;
	// Use this for initialization
	void Start () {
		SelectScene.SetActive (false);
		RankCanvas.SetActive (false);
		maxSceneCount = gameview.Length - 1;
		leaderboardController = GetComponent<LeaderBoardController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (SelectScene.active) {
			if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D )) {
				gameview [currentSelect].GetComponent<OutlineCustom> ().eraseRenderer = true;
				currentSelect = (currentSelect >= maxSceneCount)? 0 : currentSelect+1;
			} else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A )){
				gameview [currentSelect].GetComponent<OutlineCustom> ().eraseRenderer = true;
				currentSelect = (currentSelect <= 0)? maxSceneCount : currentSelect-1;
			}

			gameview [currentSelect].GetComponent<OutlineCustom> ().eraseRenderer = false;
		}
	}

    public void StartGame()
    {
		if(currentSelect == 0)
        	Application.LoadLevel(GameObject.Find("SceneManager").GetComponent<MySceneManager>().TRANS0);
		else if (currentSelect == 1)
			Application.LoadLevel(GameObject.Find("SceneManager").GetComponent<MySceneManager>().TRANS1);
		else if (currentSelect == 2)
			Application.LoadLevel(GameObject.Find("SceneManager").GetComponent<MySceneManager>().GAMESCENE3);
    }


    public void ExitGame()
    {
        Application.Quit();
    }

	public void PopUpSelectGameCanvas(){
		SelectScene.SetActive (true);
		StartScene.SetActive (false);
	}
 
	public void PopUpRankingCanvas(){
		RankCanvas.SetActive (true);
		StartScene.SetActive (false);
		StartCoroutine (leaderboardController.GetLeaderBoard (rankingChap + 1, rankingDifficulty, (lb) => {
			TextClean();
			SetLeaderBoard(lb);
		}));
	} 
    public void BackToStartScene()
    {
		RankCanvas.SetActive (false);
        SelectScene.SetActive(false);
        StartScene.SetActive(true);
    }


	public void SelectGame(int index){
		gameview [currentSelect].GetComponent<OutlineCustom> ().eraseRenderer = true;
		currentSelect = index;
	}

	public void SetRankingChap(int index){
		rankingChap = index;
		StartCoroutine (leaderboardController.GetLeaderBoard (rankingChap + 1, rankingDifficulty, (lb) => {
			TextClean();
			SetLeaderBoard(lb);
		}));
	}

	public void SetRankingDifficulty(int index){
		rankingDifficulty = index;
		StartCoroutine (leaderboardController.GetLeaderBoard (rankingChap + 1, rankingDifficulty, (lb) => {
			TextClean();
			SetLeaderBoard(lb);
		}));
	}

	private void SetLeaderBoard(LeaderBoard lb) {
		for (int i = 0; i < lb.leaderboard.Count; i++) {
			playerInfo playerInfo = lb.leaderboard [i];
			Rank.text += (i + 1).ToString () + "\n"; 
			Name.text += playerInfo.name + "\n";
			Score.text += playerInfo.score.ToString () + "\n";
			//			Debug.Log (playerInfo.name + ": " + playerInfo.score);
		}
	}

	private void TextClean() {
		Rank.text = "";
		Name.text = "";
		Score.text = "";
	}
}
