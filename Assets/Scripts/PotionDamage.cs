using UnityEngine;
using System.Collections;

public class PotionDamage : MonoBehaviour {

	public bool canBeDestroyed = false;
	public int maxHealth = 1;
	int health;

	bool flag = true;
	// Use this for initialization
	void Start () {
		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setStartToDestroyed() {
		canBeDestroyed = true;
	}


	public void TakeDamage() {
		if (canBeDestroyed && flag) {
			health--; 
			flag = false;
			Invoke ("reEnable", 3.0f);
			if (health <= 0)
			{
				canBeDestroyed = false;
				Debug.Log("deaddeaddead!!!!!!!!!!!!!!!");
				gameObject.GetComponentInChildren<BasicProperties>().NewDisability("faint", true);
				gameObject.GetComponentInChildren<Animator>().SetBool("dead", true);
				Invoke("Destroyed", 6.0f);
			}
			else {
				gameObject.GetComponentInChildren<BasicProperties>().NewDisability("faint", true);
				gameObject.GetComponentInChildren<Animator>().SetBool("faint", true);
				Invoke("WakeUp", 3f);
				Debug.Log("health " + health);
			}
		}
	}

	void reEnable(){
		flag = true;
	}

	private void WakeUp()
	{
		gameObject.GetComponentInChildren<BasicProperties>().NewDisability("faint", false);
		gameObject.GetComponentInChildren<Animator>().SetBool("faint", false);
	}

	void Destroyed() {
		Destroy(gameObject);
	}
}
