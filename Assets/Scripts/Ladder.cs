using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {
	public float climbSpeed = 0.1f;
    public GameObject Temp;
    public GameObject NormalLadder;
    public GameObject IceLadder;
    public float ICE_TEMP = 13.0f;

    private TempController tempController;
	// Use this for initialization
	void Start () {
       
        IceLadder.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () { 

        if (GameObject.Find("TempHandle").GetComponent<TempController>().GetTemp() < ICE_TEMP)
        {
            IceLadder.SetActive(true); NormalLadder.SetActive(false);
        }
        else
        {
            NormalLadder.SetActive(true); IceLadder.SetActive(false);
        }
        
	}
    
	void OnTriggerStay(Collider col) 
	{
        //print(col.gameObject.name + " stay");
        if (col.gameObject.tag == "Player" && Input.GetKey(KeyCode.W) && NormalLadder.active)
        {
			col.GetComponent<Character> ().climbing = true;
			col.gameObject.GetComponent<Rigidbody> ().useGravity = false;
			col.transform.position = new Vector3 (transform.position.x, col.transform.position.y, col.transform.position.z);
			col.transform.position += new Vector3 (0, climbSpeed, 0);
        }
        else if (col.gameObject.tag == "Player" && Input.GetKey(KeyCode.S) && NormalLadder.active)
        {
            col.GetComponent<Character>().climbing = true;
            col.GetComponent<Character>().ladder = this.transform;
            col.gameObject.GetComponent<Rigidbody>().useGravity = false;
            col.transform.position = new Vector3(transform.position.x, col.transform.position.y, col.transform.position.z);
            col.transform.position -= new Vector3(0, climbSpeed, 0);
        }
        else if (col.gameObject.tag == "Player" && !NormalLadder.active)
        {
			col.GetComponent<Character>().ladder = this.transform;
            col.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
	}

	void OnTriggerExit(Collider col)
	{
        //print(col.gameObject.name + " exit");
        if (col.gameObject.tag == "Player" && NormalLadder.active) 
		{
			col.GetComponent<Character> ().climbing = false;
			col.gameObject.GetComponent<Rigidbody> ().useGravity = true;
		}
	}
}
