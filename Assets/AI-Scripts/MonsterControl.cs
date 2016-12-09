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
        movingSpeed = 0f;
		Vector3 characterPosition = GameObject.Find ("Character").transform.position;
		float xDifference = characterPosition.x - monster.transform.position.x;
		float sign = Mathf.Abs (xDifference) < 0.5 ? 0: xDifference / Mathf.Abs(xDifference);

        //pass parameters to character script, and then it can move
        monster.Move(movingSpeed * sign, jump);

        //jump is reset after each time that physical engine updated
        jump = false;
    }
}
