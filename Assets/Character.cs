using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public float maxSpeed = 10.0f;
	public float climbSpeed = 0.1f;
    public float jumpForce = 800.0f;

    public bool airControl = true;

    bool facingRight;

    public LayerMask groundLayer;

    Transform groundCheck;
    float groundRadius;
    bool onGround;
	bool onLadder;
	bool reEnableFloorCol;

    Animator anim;
	Collision floor;

    void Awake()
    {
        //get references
        groundCheck = transform.Find("GroundCheck");
        anim = transform.Find("Canvas/Image").GetComponent<Animator>();
        
    }

    // Use this for initialization
    void Start()
    {
        facingRight = true;
        groundRadius = 0.1f;
        onGround = false;
    }

    void FixedUpdate()
    {
        
         //change the character animation by onGround state
       anim.SetBool("onGround", onGround);
    }

	void Update() 
	{
		if (reEnableFloorCol && transform.position.y > floor.transform.position.y + 0.8) {
			floor.collider.enabled = true;
		}
	}

    public void Move(float movingSpeed, bool jump)
    {
        //left / right moving actived only when the character is on the ground or air control is premitted
        if (onGround || airControl)
        {
            //change the character animation by moving speed
            anim.SetFloat("Speed", Mathf.Abs(movingSpeed));
            //move the character
            //only change its velocity on x axis
            transform.position += new Vector3(movingSpeed * maxSpeed, GetComponent<Rigidbody>().velocity.y * Time.deltaTime, 0);
            // GetComponent<Rigidbody>().velocity = new Vector2(movingSpeed * maxSpeed, GetComponent<Rigidbody>().velocity.y);

            //flip the character image if player input direction is different with character's facing direction
            if (movingSpeed > 0 && !facingRight || movingSpeed < 0 && facingRight) Flip();
        }

        //let character jump when it's on the ground and player hits jump button
        if (onGround && jump)
        {
            anim.SetBool("onGround", false);

            //make character jump by adding force
            GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, jumpForce, 0.0f));
        }
    }

	public void Climb()
	{
		if (onLadder) {
			anim.SetFloat ("ClimbSpeed", Mathf.Abs (climbSpeed));
			print ("Climb");
			// transform.position = new Vector3 (ladder.position.x, transform.position.y, transform.position.z);
			transform.position += new Vector3 (0, climbSpeed, 0);
		}
	}

    void Flip()
    {
        //reverse the direction
        facingRight = !facingRight;

        //flip the character by multiplying x local scale with -1
        Vector3 characterScale = transform.localScale;
        characterScale.x *= -1;
        transform.localScale = characterScale;
    }

    void OnCollisionEnter(Collision col)
    {
		if (col.gameObject.tag == "Ground") {
			if (col.transform.position.y < transform.position.y) {
				onGround = true;
			}
			else if (col.transform.position.y > transform.position.y) {
				col.collider.enabled = false;	
				floor = col;
				reEnableFloorCol = true;
			}
		} 

    }


}
