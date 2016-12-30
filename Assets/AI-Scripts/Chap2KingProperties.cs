using UnityEngine;
using System.Collections;

public class Chap2KingProperties : MonoBehaviour {

	private GameObject player;
	private NavMeshAgent agent;
	private BasicProperties basicProperties;

    bool canBeDestroyed = false;
    const int maxHealth = 1;
    int health = maxHealth;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		basicProperties = GetComponent<BasicProperties> ();
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
//		print ("player: " + player.transform.position);
//		print ("monster: " + transform.position);
//		print ("monster: "+transform.position.y);
//		print ("player: "+player.transform.position.y);
		Invoke ("teleport", 2.0f);
	}

	private void teleport(){
//		agent.Stop ();
//        if (player)
//		    transform.position = new Vector3 (transform.position.x, player.transform.position.y - 0.7f, transform.position.z);
////		agent.ResetPath ();
//		Invoke ("teleport", 2.0f);
		Invoke("test", 3.0f);
	}

	private void test() {
//		agent.Resume ();

	}

    public void setStartToDestroyed() {
        canBeDestroyed = true;
    }

    public void TakeDamage() {
        if (canBeDestroyed) {
            health--; 
            
            if (health <= 0)
            {
                canBeDestroyed = false;
                Debug.Log("deaddeaddead!!!!!!!!!!!!!!!");
                gameObject.GetComponent<BasicProperties>().NewDisability("faint", true);
                gameObject.GetComponentInChildren<Animator>().SetBool("dead", true);
                Invoke("Destroyed", 6.0f);
            }
            else {
                gameObject.GetComponent<BasicProperties>().NewDisability("faint", true);
                gameObject.GetComponentInChildren<Animator>().SetBool("faint", true);
                Invoke("WakeUp", 2f);
                Debug.Log("health " + health);
            }
        }
    }

    private void WakeUp()
    {
        gameObject.GetComponent<BasicProperties>().NewDisability("faint", false);
        gameObject.GetComponentInChildren<Animator>().SetBool("faint", false);
    }

    void Destroyed() {
        Destroy(gameObject);
    }
}
