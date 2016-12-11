using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    public float maxSpeed = 10.0f;
	public float climbSpeed = 0.1f;
    public float jumpForce = 800.0f;

    public bool airControl = true;
	public bool climbing = false;
	public bool grounded = true;

    [SerializeField]
    private List<GameObject> ground;

    bool facingRight;



	LayerMask playerLayer;
	LayerMask groundLayer;

    Transform groundCheck;
    float groundRadius;
    bool onGround;

    Animator anim;
    AudioSource audioSource;

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
        audioSource = GetComponentInChildren<AudioSource>();
        playerLayer = LayerMask.NameToLayer("Player");
		groundLayer = LayerMask.NameToLayer ("Ground");
		//print (playerLayer.value);
        facingRight = true;
        groundRadius = 0.1f;
        onGround = false;
    }

    void FixedUpdate()
    {
         //change the character animation by onGround state
        anim.SetBool("onGround", onGround);
        anim.SetBool("climbing", climbing);
    }

	void Update() 
	{
		grounded = Physics.CheckSphere (groundCheck.position, groundRadius, 1 << groundLayer.value);
		if (ladder && transform.position.y < ladder.position.y && grounded)
			Physics.IgnoreLayerCollision (playerLayer.value, groundLayer.value, false);
		else
			Physics.IgnoreLayerCollision ( playerLayer.value, groundLayer.value, climbing);
		//print ("CLIMBING: " + climbing);

	}

    public void Move(float movingSpeed, bool jump, float upSpeed)
    {
        //left / right moving actived only when the character is on the ground or air control is premitted
        if (grounded || airControl)
        {
            //change the character animation by moving speed
            anim.SetFloat("Speed", Mathf.Abs(movingSpeed));
            if (Mathf.Abs(movingSpeed) > 0)
            {
                if(!audioSource.isPlaying)
                    audioSource.PlayOneShot(audioSource.clip);
            }
            else
            {
                audioSource.Stop();
            }
                
            //move the character
            //only change its velocity on x axis
            transform.position += new Vector3(movingSpeed * maxSpeed, GetComponent<Rigidbody>().velocity.y * Time.deltaTime, 0);
            // GetComponent<Rigidbody>().velocity = new Vector2(movingSpeed * maxSpeed, GetComponent<Rigidbody>().velocity.y);

            //flip the character image if player input direction is different with character's facing direction
            if (movingSpeed > 0 && !facingRight || movingSpeed < 0 && facingRight) Flip();
        }
        if (climbing)
        {
            anim.SetFloat("UpSpeed", Mathf.Abs(upSpeed));
        }

        //let character jump when it's on the ground and player hits jump button
        if (grounded && jump)
        {
            anim.SetBool("onGround", false);

            //make character jump by adding force
            GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, jumpForce, 0.0f));
        }
       
    }

    public void Jump()
    {
        anim.SetBool("onGround", false);
        GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, jumpForce, 0.0f));
    }

    public void DisablePlayerMove()
    {
        GetComponent<CharacterControl>().enableMove = false;
    }

    public void EnablePlayerMove()
    {
        GetComponent<CharacterControl>().enableMove = true;
    }
		

    public void Flip()
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
		//print (col);
		if (col.gameObject.tag == "Ground") {
			if (col.transform.position.y < transform.position.y) {
				onGround = true;
				climbing = false;
				for (int i = 0; i < 3; i++)
				{
					Physics.IgnoreCollision(GetComponent<Collider>(), this.ground[i].GetComponent<Collider>(), false);
					GetComponent<Rigidbody>().useGravity = true;
					if (col.gameObject == ground[i])
					{
						GetComponent<AIInformation>().Floor = i + 1;
					}
				}
            }
        }
    }

    public void setFloor() {
        ground[0] = GameObject.Find("Floor");
        ground[1] = GameObject.Find("Floor (1)");
        ground[2] = GameObject.Find("Floor (2)");
    }

}
