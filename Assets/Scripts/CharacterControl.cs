using UnityEngine;
using System.Collections;
public class CharacterControl : MonoBehaviour
{ 
    private Character character;
    private bool jump;
    public bool enableMove;
    private float movingSpeed, upSpeed;

    // Use this for initialization
    void Start()
    {
        enableMove = true;
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
		if (UserControl.IncreaseTemp) {
			print ("temp ++");
			GameObject.Find ("TempHandle").GetComponent<TempController> ().IncreaseTemp ();
		}
		if (UserControl.DecreaseTemp) {
			print("temp --");
			GameObject.Find ("TempHandle").GetComponent<TempController> ().DecreaseTemp ();
		}
        //get jump input by "jump" button set in input setting
//        if (Input.GetButtonDown("Jump")) jump = true;
    }

    void FixedUpdate()
    {
		
        //get input by Axis set in input setting
        if (enableMove)
            movingSpeed = Input.GetAxis("Horizontal");
        else
        {
            movingSpeed = 0;
        }
        upSpeed = Input.GetAxis("Vertical");
        //pass parameters to character script, and then it can move
        character.Move(movingSpeed, jump, upSpeed);
	
        //jump is reset after each time that physical engine updated
        jump = false;

    }
}
