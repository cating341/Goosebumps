using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

    public GameObject timerText;
    public GameObject totalTimerText; 
    public GameObject gameoverCanvas;

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
        Application.LoadLevel(1);
    }

    public void Exit()
    {
        Application.Quit();
    }


}
