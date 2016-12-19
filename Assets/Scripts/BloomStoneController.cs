using UnityEngine;
using System.Collections;

public class BloomStoneController : MonoBehaviour {

    public GameObject pointLight;

    GameObject TreeController;
    bool enable = true;
	// Use this for initialization
	void Start () {
        TreeController = GameObject.Find("MagicTree");
        pointLight.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player" && Input.GetKeyDown("z") && enable)
        {
            TreeController.GetComponent<MagicTreeController>().LightTheStone();
            gameObject.GetComponent<LensFlare>().enabled = true;
            pointLight.SetActive(true);
            enable = false;
        }
    }
}
