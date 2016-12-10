using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (player.transform.position.x <= -4.58f)
            gameObject.transform.position = new Vector3(-4.58f, gameObject.transform.position.y, -15.97f);
        else if (player.transform.position.x >= 3.53f)
            gameObject.transform.position = new Vector3(3.53f, gameObject.transform.position.y, -15.97f);
        else
            gameObject.transform.position = new Vector3(player.transform.position.x, gameObject.transform.position.y, -15.97f);

        if (player.transform.position.y <= -0.32f)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, -0.32f, -15.97f);
        else if (player.transform.position.y >= 3.3f)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 3.3f, -15.97f);
        else
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, player.transform.position.y, -15.97f);
        
	}
}
