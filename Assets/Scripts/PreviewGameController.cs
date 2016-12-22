using UnityEngine;
using System.Collections;

public class PreviewGameController : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GameStart()
    {
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -5.0f);
		player.GetComponent<Character>().EnablePlayerMove();
		if(GameObject.Find("SceneManager").GetComponent<MySceneManager>().currentSceneName == GameObject.Find("SceneManager").GetComponent<MySceneManager>().PREVIEW1 )
			Application.LoadLevel(GameObject.Find("SceneManager").GetComponent<MySceneManager>().GAMESCENE1);
		else if(GameObject.Find("SceneManager").GetComponent<MySceneManager>().currentSceneName == GameObject.Find("SceneManager").GetComponent<MySceneManager>().PREVIEW2 )
			Application.LoadLevel(GameObject.Find("SceneManager").GetComponent<MySceneManager>().GAMESCENE2);
    }
}
