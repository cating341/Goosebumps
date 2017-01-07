using UnityEngine;
using System.Collections;

public class MagicTreeController : MonoBehaviour {

    public int MAXSTONE = 5;
    public GameObject TreeBridge;
    int count = 0;
	// Use this for initialization
	void Start () {
        TreeBridge.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LightTheStone() {
        count++;
        if (count >= MAXSTONE )
        {
            Camera.main.GetComponent<CameraController>().CameraMovement();
			if (GameObject.Find ("GameHandle").GetComponent<GameController>() ){
				GameObject.Find ("GameHandle").GetComponent<GameController> ().setKingMonsterDestroy ();
				GameObject.Find ("GameHandle").GetComponent<GameController> ().disableMonster ();
			} else {
				GameObject.Find ("GameHandle").GetComponent<PreviewGameController> ().disableMonster ();
			}

            TreeBridge.SetActive(true);
            count = 0;
			if (GameObject.Find("KingMonster2(Clone)") ) 
				GameObject.Find("KingMonster2(Clone)").GetComponent<PotionDamage>().setStartToDestroyed();
			
        }
    }
}
