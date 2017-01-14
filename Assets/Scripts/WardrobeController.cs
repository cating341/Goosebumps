using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WardrobeController : MonoBehaviour {
    
	public Transform outpos;
	public Transform inpos;

    private AudioSource audioSource;
    private Animator animation;
    private bool enable = true, hide = false;

    GameObject player;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        animation = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player");
    }

	void Update () {
	
	}
    void OnTriggerStay(Collider other)
    {
		if (other.gameObject.tag == "Player" && UserControl.UseItem) {
			if (enable) {
				enable = false;
				if (!hide) {
					PlaySoundEffect ();
					Invoke ("PlayerHidden", 1.0f);
				} else {
					ScratchYouOut ();
				}
			}
		} else if (other.gameObject.name == "KingMonster1" && hide ) {
			ScratchYouOut ();
			hide = false;

		}
    }
    void PlaySoundEffect()
    {
        audioSource.Play();
        animation.SetTrigger("open");
    }

	private void ScratchYouOut() {
		PlaySoundEffect ();
		//player.GetComponent<SpriteRenderer>().enabled = true;
		player.transform.position = new Vector3 (outpos.position.x , player.transform.position.y, outpos.position.z);

		player.GetComponent<Character> ().EnablePlayerMove ();
		Invoke ("ReEnable", 3.0f);
	}

    void PlayerHidden()
    {
        enable = true;
        hide = true;
		player.transform.position = new Vector3 (inpos.position.x , player.transform.position.y, inpos.position.z);
        player.GetComponent<Character>().DisablePlayerMove();
    }

    void ReEnable()
    {
        enable = true;
        hide = false;
    }

}
