using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

    private int movingForward = 1;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        //GetComponent<Rigidbody>().velocity = new Vector3(2 * movingForward, 0, 0);
        if (GetComponent<Rigidbody>().position.x > 10)
        {
            movingForward = -1;
        }
        else if (GetComponent<Rigidbody>().position.x < -10)
        {
            movingForward = 1;
        }
        print(GetComponent<Rigidbody>().position.x);
    }
}
