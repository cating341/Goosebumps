using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WardrobeController : MonoBehaviour {
    
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
		if (other.gameObject.tag == "Player" && Input.GetKeyDown ("z")) {
			if (enable) {
				enable = false;
				if (!hide) {
					PlaySoundEffect ();
					Invoke ("PlayerHidden", 1.0f);
				} else {
					ScratchYouOut ();
				}
			}
		} else if (other.gameObject.name == "KingMonster1" && hide) {
			ScratchYouOut ();
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
		player.transform.position -= new Vector3 (0, 0, 2.0f);
		player.GetComponent<Character> ().EnablePlayerMove ();
		Invoke ("ReEnable", 3.0f);
	}

    void PlayerHidden()
    {
        enable = true;
        hide = true;
        player.transform.position += new Vector3(0, 0, 2.0f);
        player.GetComponent<Character>().DisablePlayerMove();
    }

    void ReEnable()
    {
        enable = true;
        hide = false;
    }

}
