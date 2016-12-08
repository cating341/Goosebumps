using UnityEngine;
using System.Collections;

public class MonsterControl : MonoBehaviour {

    private Monster monster;
    private bool jump;
    private float movingSpeed;

    // Use this for initialization
    void Start()
    {
        monster = GetComponent<Monster>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        //get input by Axis set in input setting
        movingSpeed = Input.GetAxis("Horizontal");

        //pass parameters to character script, and then it can move
        monster.Move(movingSpeed, jump);

        //jump is reset after each time that physical engine updated
        jump = false;
    }
}
