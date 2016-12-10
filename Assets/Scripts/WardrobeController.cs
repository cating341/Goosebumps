using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WardrobeController : MonoBehaviour {
    public Image playerImage;
    private AudioSource audioSource;
    private Animator animation;
    private bool enable = true, hide = false;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        animation = GetComponentInChildren<Animator>();
    }

	void Update () {
	
	}
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown("z"))
        {
            if (enable)
            {
                enable = false;
                if (!hide)
                {
                    PlaySoundEffect();
                    Invoke("PlayerHidden", 1.0f);
                }
                else
                {
                    PlaySoundEffect();
                    playerImage.GetComponent<SpriteRenderer>().enabled = true;
                    Invoke("ReEnable", 3.0f);
                }
            }
        }
    }
    void PlaySoundEffect()
    {
        audioSource.Play();
        animation.SetTrigger("open");
    }

    void PlayerHidden()
    {
        enable = true;
        hide = true;
        playerImage.GetComponent<SpriteRenderer>().enabled = false;
    }

    void ReEnable()
    {
        enable = true;
        hide = false;
    }

}
