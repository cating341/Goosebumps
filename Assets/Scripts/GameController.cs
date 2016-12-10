using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

    public GameObject timerText;
    public GameObject totalTimerText; 
    public GameObject gameoverCanvas;


    float timer = 0;
    float currentTimer = 0;

	// Use this for initialization
	void Start () {
        gameoverCanvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        
            timer += Time.deltaTime;
            currentTimer += Time.deltaTime;
            if (timer > 1.0f)
            {
                timerText.GetComponent<Text>().text = ((int)currentTimer).ToString();
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
        Application.LoadLevel(0);
    }

    public void Exit()
    {
        Application.Quit();
    }


}
