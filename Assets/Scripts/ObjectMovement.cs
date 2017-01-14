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
        
        if (other.gameObject.tag == "Player" && Input.GetKey(UserControl.UseItemKey) )
        {
            if (gameObject.tag == "Potion")
                gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y-0.86f, gameObject.transform.position.z);
            else
                gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }

}
