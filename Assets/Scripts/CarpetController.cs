using UnityEngine;
using System.Collections;

public class CarpetController : MonoBehaviour {

    public GameObject Temp;

    private TempController tempController;
    private const float HEAT_TEMP = 65.0f;
    private Animator anim;
    private float pre_temp;
	// Use this for initialization
	void Start () {
        tempController = Temp.GetComponent<TempController>();
        anim = GetComponentInChildren<Animator>();
        pre_temp = tempController.GetTemp();
	}
	
	// Update is called once per frame
	void Update () {
        if (pre_temp != tempController.GetTemp())
        {
            pre_temp = tempController.GetTemp();
            if (tempController.GetTemp() > HEAT_TEMP)
            {
                anim.SetBool("fire", true);
            }
            else
            {
                anim.SetBool("fire", false);
            }
        }

	}

    void OnTriggerStay(Collider other)
    {
       
        if (other.gameObject.tag == "Player" && anim.GetBool("fire"))
        {
            Debug.Log("Onfire!! hurt!!");
        }
    }
}
