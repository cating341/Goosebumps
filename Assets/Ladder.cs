using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {
	public float climbSpeed = 0.1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void OnTriggerStay(Collider col) 
	{
		print (col);
		if (col.gameObject.tag == "Player" && Input.GetKey (KeyCode.W)) {
			col.GetComponent<Character> ().climbing = true;
			col.gameObject.GetComponent<Rigidbody> ().useGravity = false;
			col.transform.position = new Vector3 (transform.position.x, col.transform.position.y, col.transform.position.z);
			col.transform.position += new Vector3 (0, climbSpeed, 0);
		} else if (col.gameObject.tag == "Player" && Input.GetKey (KeyCode.S)) {
			col.GetComponent<Character> ().climbing = true;
			col.GetComponent<Character> ().ladder = this.transform;
			col.gameObject.GetComponent<Rigidbody> ().useGravity = false;
			col.transform.position = new Vector3 (transform.position.x, col.transform.position.y, col.transform.position.z);
			col.transform.position -= new Vector3 (0, climbSpeed, 0);
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Player") 
		{
			print ("exit");
			col.GetComponent<Character> ().climbing = false;
			col.gameObject.GetComponent<Rigidbody> ().useGravity = true;
		}
	}
}
