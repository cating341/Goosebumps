using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 
using System.Collections.Generic;

public class MySceneManager : MonoBehaviour {
     
    GameObject gameController;
    List<GameObject> gearList = new List<GameObject>();
    GameObject player;

	// Use this for initialization
	void Start () {
        GameObject.DontDestroyOnLoad(gameObject);
        gameController = GameObject.Find("GameHandle"); 
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    // For any gear that player put in the scene
    public void addToGearList(GameObject g) {
        GameObject.DontDestroyOnLoad(g);
        gearList.Add(g);
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name + " " + scene.buildIndex );
        Debug.Log(mode);
        if (scene.buildIndex != 0) {  // if not preview, mean game start!

        }
        else if (scene.buildIndex == 0) { // reload preview scene
            foreach(GameObject g in gearList){
                Destroy(g);
            } 
        }
    }
     
}
