using UnityEngine;
using System.Collections;

public class PotionController : MonoBehaviour {
	public float HEAT_TEMP = 70.0f;
    public AudioClip bombSound;
	Animator animation;
    bool bombstate;
	// Use this for initialization
	void Start () {
		animation = gameObject.GetComponentInChildren<Animator> ();
        bombstate = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find("TempHandle").GetComponent<TempController>().GetTemp() > HEAT_TEMP && !animation.GetBool("explode"))
		{
			animation.SetBool("explode", true);
            Invoke("PlaySound", 1.3f);
			Invoke ("DestroyObject", 2.8f);
		}
	}

	void DestroyObject(){

		Destroy (gameObject);
	}

    void PlaySound() {
        gameObject.GetComponent<AudioSource>().Stop();
        gameObject.GetComponent<AudioSource>().volume = 1.0f;
        gameObject.GetComponent<AudioSource>().maxDistance = 50.0f;
        gameObject.GetComponent<AudioSource>().PlayOneShot(bombSound);
        bombstate = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Monster" && bombstate && GameObject.Find("SceneManager").GetComponent<MySceneManager>().GameSceneIsGame())
        {
            //if(other.gameObject.layer == LayerMask.NameToLayer("KingMonster")){
				other.gameObject.GetComponentInParent <PotionDamage>().TakeDamage();
			
        }
    }
    
}
