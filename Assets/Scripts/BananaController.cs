using UnityEngine;
using System.Collections;

public class BananaController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Monster" || other.gameObject.tag == "Player")
        {

            Debug.Log("Trigger banana event");
        }
    }
}
