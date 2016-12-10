using UnityEngine;
using System.Collections;

public class BoundWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        GameObject g = collision.gameObject;
        if (g.tag == "Player") {
            collision.gameObject.transform.position = collision.gameObject.transform.position;
        }


    }
}
