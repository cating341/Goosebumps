using UnityEngine;
using System.Collections;

public class ChandelierController : MonoBehaviour {

    private const int MAXHITTIMES = 10;
    private Animator animation;
    int hitting = 0;
    bool enable = true;
	// Use this for initialization
	void Start () {
        animation = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (enable && hitting > MAXHITTIMES)
        {
            //gameObject.transform.position -= new Vector3(0, 2.8f, 0);
            animation.SetBool("falldown", true);
            enable = false;
            Invoke("takeoffOutline", 1.2f);
        }
	}

    void OnTriggerEnter(Collider other)
    { 
        if (enable && other.gameObject.tag == "Player")
        {
            hitting++;
        }
    }

    void takeoffOutline()
    {
        Destroy(gameObject.GetComponentInChildren<OutlineCustom>());
    }
}
