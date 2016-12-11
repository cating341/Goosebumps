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
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -3.92f);
        Application.LoadLevel(1); 
    }
}
