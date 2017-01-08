using UnityEngine;
using System.Collections;

public class GravestoneController : MonoBehaviour {

    public bool shine;
    private GameObject steppedMonster;
    Animator anim;
    bool monsterTrigger;
   

    void Start () {
        shine = false;
        monsterTrigger = false;
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    void Update () {
	}

    void OnTriggerEnter(Collider other)
    { 
       
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey("z"))
        {
            shine = true;
            anim.SetBool("shine", true);
            Invoke("WakeUp", 3f);
        }

		if (other.gameObject.tag == "Monster" && shine)
		{
			monsterTrigger = true;
			steppedMonster = other.gameObject;
			steppedMonster.GetComponent<BasicProperties>().NewDisability("faint", true);
			steppedMonster.GetComponentInChildren<Animator>().SetBool("faint", true);
			shine = false;
			Debug.Log("Trigger shine gravestone event");
		}
    }

    private void WakeUp()
    {
        shine = false;
        anim.SetBool("shine", false);
        if (monsterTrigger)
        {
            monsterTrigger = false;
            steppedMonster.GetComponent<BasicProperties>().NewDisability("faint", false);
            steppedMonster.GetComponentInChildren<Animator>().SetBool("faint", false);
        }
    }

}
