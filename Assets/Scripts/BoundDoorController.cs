using UnityEngine;
using System.Collections;

public class BoundDoorController : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collision)
    {
		if (collision.gameObject.layer == LayerMask.NameToLayer( "SoldierMonster"))
        {
			collision.gameObject.GetComponent<SoldierProperties> ().TurnWay ();
            Destroy(gameObject);
        } 
    }
}
