using UnityEngine;
using System.Collections;

public class Chap2KingProperties : MonoBehaviour {

	private GameObject player;
	private BasicProperties basicProperties;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		basicProperties = GetComponent<BasicProperties> ();
	}
	
	// Update is called once per frame
	void Update () {
//		print ("player: " + player.transform.position);
//		print ("monster: " + transform.position);
		Invoke ("teleport", 2.0f);
	}

	private void teleport(){
		transform.position = new Vector3 (transform.position.x, player.transform.position.y, transform.position.z);
		Invoke ("teleport", 2.0f);
	}
}
