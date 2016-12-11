using UnityEngine;
using System.Collections;

public class WaterController : MonoBehaviour {
    public GameObject Temp;
    public GameObject Water;
    public GameObject Ice;
    public float ICE_TEMP = 13.0f;

    private TempController tempController;
    private float pre_temp;
	// Use this for initialization
	void Start () {
        tempController = Temp.GetComponent<TempController>();
        pre_temp = tempController.GetTemp();
        Ice.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (pre_temp != tempController.GetTemp())
        {
            pre_temp = tempController.GetTemp();
            if (tempController.GetTemp() < ICE_TEMP)
            {
                Ice.SetActive(true); Water.SetActive(false);
            }
            else
            {
                Water.SetActive(true); Ice.SetActive(false);
            }
        }
	}

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player" && tempController.GetTemp() < ICE_TEMP)
        {
            Debug.Log("ICEEEE!! hurt!!");
        }
		else if(other.gameObject.tag == "Monster")
        {
			if (tempController.GetTemp () < ICE_TEMP)
				other.gameObject.GetComponent<Monster> ().Disabled = true;
			else {
				other.gameObject.GetComponent<Monster> ().Disabled = false;
			}
        }
    }
}
