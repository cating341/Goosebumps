using UnityEngine;
using System.Collections;

public class ObjectMovement : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

     
    void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "Player" && Input.GetKey("z") )
        {
            gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }

}
