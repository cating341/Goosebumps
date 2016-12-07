using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        GameObject player = other.gameObject;
        if (player.tag == "Player" && Input.GetKeyDown("z"))
        {
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
            foreach (GameObject door in doors)
            {
                if (!Vector3.Equals(door.transform.position, gameObject.transform.position))
                {
                    player.transform.position = new Vector3(door.transform.position.x, door.transform.position.y, player.transform.position.z);
                }
            }
        }
    }


}
