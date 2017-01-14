using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	bool enable = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        GameObject player = other.gameObject;
		if (player.tag == "Player" && UserControl.UseItem)
        {
			//print ("enter door");
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
            foreach (GameObject door in doors)
            {
				if (!Vector3.Equals(door.transform.position, gameObject.transform.position) && enable)
                {
                    player.transform.position = new Vector3(door.transform.position.x, door.transform.position.y+1.0f, player.transform.position.z);
					door.GetComponent<DoorController> ().Disable ();
				}
            }
        }
    }

	public void Disable(){
		enable = false;
		Invoke ("ReEnable", 0.3f);
	}

	void ReEnable(){
		enable = true;
	}


}
