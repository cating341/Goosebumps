using UnityEngine;
using System.Collections;

public class TvController : MonoBehaviour {

    private bool open = false;
    private Animator animation;
	// Use this for initialization
	void Start () {
        animation = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        animation.SetBool("on", open);
	}

    public void TrunOnTV(){
        open = true;
        Invoke("TrunOffTV", 5.0f);
    }

    void TrunOffTV() {
        open = false;
    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player" && Input.GetKey("z"))
        {
            TrunOnTV();
        }
    }
}
