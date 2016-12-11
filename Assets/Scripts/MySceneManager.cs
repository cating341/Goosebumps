using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 
using System.Collections.Generic;

public class MySceneManager : MonoBehaviour {

    public int sceneIndex;

    GameObject gameController;
    List<GameObject> gearList = new List<GameObject>();
    List<GameObject> sceneList = new List<GameObject>();
    GameObject player;

    bool flag = false;
    public static MySceneManager ins;
    void Awake() {
        if (ins == null)
        {
            ins = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
        else if (ins != this)
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("GameHandle");
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    // For any gear that player put in the scene
    public void addToGearList(GameObject g) {
        GameObject.DontDestroyOnLoad(g);
        gearList.Add(g);
    }

    public void removeFromGearList(GameObject g)
    {
        gearList.Remove(g);
    }

    public void addToSceneList(GameObject g)
    {
        GameObject.DontDestroyOnLoad(g);
        sceneList.Add(g);
    }

    public void removeAllFromSceceList( )
    {
        foreach (GameObject g in sceneList)
        {
            Destroy(g);
        }
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

        sceneIndex = scene.buildIndex;

        if (scene.buildIndex == 1) {  // if not preview, mean game start!
            foreach (GameObject g in gearList)
            {
                Destroy(g.GetComponent<PickUpItem>());
            }

            player.GetComponent<Character>().setFloor();
        }
        else if (scene.buildIndex == 0) { // reload preview scene
            foreach(GameObject g in gearList){
                Destroy(g);
            } 
        }
    }
     
}
