using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {
	public float climbSpeed = 0.1f;
    public GameObject Temp;
    public GameObject NormalLadder;
    public GameObject IceLadder;
    public float ICE_TEMP = 13.0f;

    private TempController tempController;
    private float pre_temp;
	// Use this for initialization
	void Start () {
        Temp = GameObject.Find("TempHandle");
        tempController = Temp.GetComponent<TempController>();
        pre_temp = tempController.GetTemp();
        IceLadder.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (pre_temp != tempController.GetTemp())
        {
            pre_temp = tempController.GetTemp();
            if (tempController.GetTemp() < ICE_TEMP)
            {
                IceLadder.SetActive(true); NormalLadder.SetActive(false);
            }
            else
            {
                NormalLadder.SetActive(true); IceLadder.SetActive(false);
            }
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
            col.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
	}

	void OnTriggerExit(Collider col)
	{
        //print(col.gameObject.name + " exit");
        if (col.gameObject.tag == "Player" && NormalLadder.active) 
		{
			print ("exit");
			col.GetComponent<Character> ().climbing = false;
			col.gameObject.GetComponent<Rigidbody> ().useGravity = true;
		}
	}
}
