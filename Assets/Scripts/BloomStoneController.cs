using UnityEngine;
using System.Collections;

public class BloomStoneController : MonoBehaviour {

    public GameObject pointLight;

    GameObject TreeController;
	GameController gameHandle;
    GameObject player;
    bool enable = true;
	// Use this for initialization
	void Start () {
        TreeController = GameObject.Find("MagicTree");
        player = GameObject.Find("Player");
		gameHandle = GameObject.Find ("GameHandle").GetComponent<GameController> ();
        pointLight.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        float y = player.transform.position.y - gameObject.transform.position.y;
        if ((Vector3.Distance(player.transform.position, gameObject.transform.position) < 3.7f && Mathf.Abs(y) < 2.5f) || !enable)
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        else
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 1);
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
