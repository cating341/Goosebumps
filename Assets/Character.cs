using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public float maxSpeed = 10.0f;
	public float climbSpeed = 0.1f;
    public float jumpForce = 800.0f;

    public bool airControl = true;
	public bool climbing = false;
	public bool grounded = true;


    bool facingRight;

    

	const int playerLayer = 11;
	const int groundLayer = 8;

    Transform groundCheck;
    float groundRadius;
    bool onGround;
	bool onLadder;
	bool belowFloorCol;
	bool aboveFloorCol;

	public LayerMask layer;
    Animator anim;
	Collision aboveFloor;
	Collision belowFloor;

	public Transform ladder;

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
//		if (climbing) {
//			
//			grounded = Physics.CheckSphere (groundCheck.position, groundRadius, layer);
//		} else {
//			grounded = false;
//		}
		grounded = Physics.CheckSphere (groundCheck.position, groundRadius, layer);
		if (ladder && transform.position.y < ladder.position.y && grounded)
			Physics.IgnoreLayerCollision (playerLayer, groundLayer, false);
		else
			Physics.IgnoreLayerCollision (playerLayer, groundLayer, climbing);

		print (climbing);




//		if (aboveFloorCol && transform.position.y > aboveFloor.transform.position.y + 0.8) {
//			aboveFloor.collider.enabled = true;
//			aboveFloorCol = false;
//		} else if (aboveFloorCol && transform.position.y < belowFloor.transform.position.y - 1) {
//			belowFloor.collider.enabled = true;
//			belowFloorCol = false;
//		}
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
		print (col);
		if (col.gameObject.tag == "Ground") {
			if (col.transform.position.y < transform.position.y) {
				onGround = true;
				climbing = false;
			}
			else if (col.transform.position.y > transform.position.y) {
//				Physics.IgnoreCollision (col.collider, GetComponent<Collider> ());
				aboveFloor = col;
				aboveFloorCol = true;

			}
		} 

    }

	void OnCollisionStay(Collision col)
	{
		if (col.gameObject.tag == "Ground") {
			if (col.transform.position.y < transform.position.y && climbing) {
//				print ("takeoffCollider");
//				col.collider.enabled = false;
				belowFloorCol = true;
				belowFloor = col;
			}
		}
	}

}
