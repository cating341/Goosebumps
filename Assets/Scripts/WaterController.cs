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
				if (GameObject.Find ("SceneManager").GetComponent<MySceneManager> ().GetLevel () == 1) {
					Physics.IgnoreLayerCollision (LayerMask.NameToLayer ("KingMonster"), LayerMask.NameToLayer ("Icecube"), false);
					Physics.IgnoreLayerCollision (LayerMask.NameToLayer ("KingMonster"), LayerMask.NameToLayer ("Water"), false);
				} else if (GameObject.Find ("SceneManager").GetComponent<MySceneManager> ().GetLevel () == 2) {
					Physics.IgnoreLayerCollision (LayerMask.NameToLayer ("KingMonster"), LayerMask.NameToLayer ("Water"), false);
				}
            }
        }
	}

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Monster")
        {
			if (tempController.GetTemp () < ICE_TEMP) {
				other.gameObject.GetComponent<BasicProperties> ().NewDisability ("water", true);
			}
			else {
				other.gameObject.GetComponent<BasicProperties> ().NewDisability ("water", false);
			}
        }
    }
}
