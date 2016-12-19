using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public GameObject player;

	private float[] gamescene1 = {-4.58f, 3.53f, -0.32f, 3.3f};
	private float[] gamescene2 = {-7.17f, 5.49f, -0.32f, 4.66f};

	float[] thisGameScece;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
		thisGameScece = gamescene2;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (player.transform.position.x <= thisGameScece[0])
			gameObject.transform.position = new Vector3(thisGameScece[0], gameObject.transform.position.y, -15.97f);
		else if (player.transform.position.x >= thisGameScece[1])
			gameObject.transform.position = new Vector3(thisGameScece[1], gameObject.transform.position.y, -15.97f);
        else
            gameObject.transform.position = new Vector3(player.transform.position.x, gameObject.transform.position.y, -15.97f);

		if (player.transform.position.y <= thisGameScece[2])
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, thisGameScece[2], -15.97f);
		else if (player.transform.position.y >= thisGameScece[3])
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, thisGameScece[3], -15.97f);
        else
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, player.transform.position.y, -15.97f);
        
	}

	public void undateCameraParameters(string i){
		if (i == GameObject.Find ("SceneManager").GetComponent<MySceneManager> ().PREVIEW1)
			thisGameScece = gamescene1;
		else if (i == GameObject.Find ("SceneManager").GetComponent<MySceneManager> ().PREVIEW2)
			thisGameScece = gamescene2;
		else 
			thisGameScece = gamescene1;
	}
}
