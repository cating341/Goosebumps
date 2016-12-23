using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

    public GameObject timerText;
    public GameObject totalTimerText; 
    public GameObject gameoverCanvas;
    public GameObject[] bakingLadders;

	[SerializeField]
	private GameObject monster;

    float timer = 0;
    float currentTimer = 0;

	// Use this for initialization
	void Start () {
        gameoverCanvas.SetActive(false);
//		Instantiate (this.monster);
//		this.monster.transform.position = new Vector3 (5.95f, 4.34f, -3.594f); 
        
	}
	
	// Update is called once per frame
	void Update () {
        
            timer += Time.deltaTime;
            currentTimer += Time.deltaTime;
            if (timer > 1.0f)
            {
                timerText.GetComponent<Text>().text = ((int)currentTimer).ToString() + " sec";
                timer = 0;
            }
        
	} 

    public void GameOver()
    {
        gameoverCanvas.SetActive(true);
        totalTimerText.GetComponent<Text>().text = ((int)currentTimer).ToString();
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
}
