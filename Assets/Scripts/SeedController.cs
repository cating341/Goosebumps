using UnityEngine;
using System.Collections;

public class SeedController : MonoBehaviour {

	public float HEAT_TEMP = 55.0f;
	public int maxHealth = 3;
	int health ;

	Animator animation;  
	private GameObject hittingThis;
	private bool attacking;
	private bool isTree;
	// Use this for initialization
	void Start () {
		animation = gameObject.GetComponentInChildren<Animator> ();
		attacking = false;
		isTree = false;
		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find("TempHandle").GetComponent<TempController>().GetTemp() > HEAT_TEMP && !isTree)
		{
			animation.SetBool("seed", true); 
			if(animation.GetCurrentAnimatorStateInfo(0).IsName("tree")) {
				TobeTree();
			}
//			Invoke ("TobeTree", 6.0f);
		}
	}

	void TobeTree(){
		isTree = true;
	}

	public void TakeDamage() {
		health--;
		animation.SetBool("hurt", true);

		Invoke("StopAni", 1.3f);
	}

	void StopAni() {
		animation.SetBool("hurt", false); 
		attacking = false; 
		if (health <= 0)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && isTree && !attacking)
		{
			/*
			this.killedMonster = other.gameObject;
			this.killedMonster.GetComponent<BasicProperties> ().NewDisability ("carpet", true);
			this.killedMonster.GetComponent<Animator> ().SetBool ("dead", true);
			*/
			attacking = true;
			/*hittingThis = other.gameObject;
			hittingThis.GetComponent<BasicProperties> ().NewDisability ("refrig", true);
			hittingThis.GetComponent<Animator> ().SetBool ("attack", true);*/
			Damage ();
		}
	}
	private void Damage(){
		TakeDamage ();
		//Invoke ("Damage", 2f);
	}
}
