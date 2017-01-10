using UnityEngine;
using System.Collections;

public class OpenScenceController : MonoBehaviour {

	public GameObject SelectScene;
	public GameObject StartScene;
	public GameObject RankCanvas;
	public GameObject[] gameview;

	int maxSceneCount = 2;
	int currentSelect = 0;

	// Use this for initialization
	void Start () {
		SelectScene.SetActive (false);
		RankCanvas.SetActive (false);
		maxSceneCount = gameview.Length - 1;
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
	}

	public void SelectGame(int index){
		gameview [currentSelect].GetComponent<OutlineCustom> ().eraseRenderer = true;
		currentSelect = index;
	}
}
