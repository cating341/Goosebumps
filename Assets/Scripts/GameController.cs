﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

    public GameObject timerText;
    public GameObject totalTimerText; 
    public GameObject gameoverCanvas;
    public GameObject nextChapBtn;
    public GameObject[] bakingLadders;
     

    float MAXTIMER = 18;
    float timer = 0;
    float currentTimer = 0;
    bool gameOver = false;

	// Use this for initialization
	void Start () {
        gameoverCanvas.SetActive(false);
        gameOver = false;
        nextChapBtn.SetActive(false); 
        
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
        
	} 

    public void GameOver()
    {
        gameOver = true;
        gameoverCanvas.SetActive(true);
        totalTimerText.GetComponent<Text>().text = ((int)currentTimer).ToString();
        if (currentTimer > MAXTIMER) 
            nextChapBtn.SetActive(true);
            
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
