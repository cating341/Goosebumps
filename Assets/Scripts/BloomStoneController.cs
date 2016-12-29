using UnityEngine;
using System.Collections;

public class BloomStoneController : MonoBehaviour {

    public GameObject pointLight;

    GameObject TreeController;
	GameController gameHandle;
    bool enable = true;
	// Use this for initialization
	void Start () {
        TreeController = GameObject.Find("MagicTree");
		gameHandle = GameObject.Find ("GameHandle").GetComponent<GameController> ();
        pointLight.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player" && Input.GetKeyDown("z") && enable)
        {
			if (gameHandle != null) {
				gameHandle.AddPoint (20.0f);
			}
            TreeController.GetComponent<MagicTreeController>().LightTheStone();
            gameObject.GetComponent<LensFlare>().enabled = true;
            pointLight.SetActive(true);
            enable = false;
        }
    }
}
