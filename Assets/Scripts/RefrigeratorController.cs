using UnityEngine;
using System.Collections;

public class RefrigeratorController : MonoBehaviour {

    public int maxHealth = 3;
    int health ;
    Animator animation;
	// Use this for initialization
	void Start () {
        health = maxHealth;
        animation = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeDamage() {
        health--;
        animation.SetBool("hurt", true);
        Invoke("StopAni", 0.5f);
    }

    void StopAni() {
        animation.SetBool("hurt", false); 
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    //Debug.Log(other.gameObject);
    //    if (other.gameObject.tag == "Player")
    //    {
    //        TakeDamage();
    //    }
    //}
}
