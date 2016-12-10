using UnityEngine;
using System.Collections;

public class CarpetController : MonoBehaviour {

    public GameObject Temp;
    public float HEAT_TEMP = 65.0f;

    private TempController tempController;
    private Animator anim;
    private float pre_temp;

    int heatCount = 0;
    float timer;
	// Use this for initialization
	void Start () {
        tempController = Temp.GetComponent<TempController>();
        anim = GetComponentInChildren<Animator>();
        pre_temp = tempController.GetTemp();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > 1.0f)
        {
            pre_temp = tempController.GetTemp();
            if (tempController.GetTemp() > HEAT_TEMP)
            {
                anim.SetBool("fire", true);
                heatCount++;
            }
            else
            {
                anim.SetBool("fire", false);
            }
            timer = 0;
        }

        if (heatCount > 10)
            Destroy(gameObject);
	}

    void OnTriggerStay(Collider other)
    {
       
        if (other.gameObject.tag == "Player" && anim.GetBool("fire"))
        {
            Debug.Log("Onfire!! hurt!!");
        }
    }
}
