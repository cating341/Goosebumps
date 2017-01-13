using UnityEngine;
using System.Collections;

public class PotionDamage : MonoBehaviour {

	public bool canBeDestroyed = false;
	const int maxHealth = 1;
	public bool canBeMinus = true;
	int health = maxHealth;

	bool flag = true;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setStartToDestroyed() {
		canBeDestroyed = true;
	}


	public void TakeDamage() {
		if (canBeDestroyed && flag) {
			if (canBeMinus) {
				health--;
				Debug.Log(gameObject.name +" health " + health);
			}

			flag = false;
			Invoke ("reEnable", 3.0f);
			if (health <= 0)
			{
				canBeDestroyed = false;
				Debug.Log("deaddeaddead!!!!!!!!!!!!!!!");
				gameObject.GetComponentInChildren<BasicProperties>().NewDisability("faint", true);
				gameObject.GetComponentInChildren<Animator>().SetBool("dead", true);
				Invoke("Destroyed", 6.0f);
				GameObject.Find("GameHandle").GetComponent<GameController>().AddPoint (100.0f);
			}
			else {
				gameObject.GetComponentInChildren<BasicProperties>().NewDisability("faint", true);
				gameObject.GetComponentInChildren<Animator>().SetBool("faint", true);
				Invoke("WakeUp", 3f);
			}
		}
	}


	public void setHealth(int settingHealth){
		health = settingHealth;
		Debug.Log (gameObject.name + " health set to " + health);
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
		GameObject.Find ("GameHandle").GetComponent<GameController> ().ReBornMonster (gameObject);
		Destroy(gameObject);
	}
}
