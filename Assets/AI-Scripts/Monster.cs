using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

    public float maxSpeed = 0.1f;
    public float jumpForce = 100f;

    public bool airControl = true;

    bool facingRight;

    public LayerMask groundLayer;
    float groundRadius;
    bool onGround;
    Animator anim;

    void Awake()
    {

    }

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        facingRight = true;
        groundRadius = 0.1f;
        onGround = false;
    }

    public void Move(float movingSpeed, bool jump)
    {
        //left / right moving actived only when the character is on the ground or air control is premitted
        if (onGround || airControl)
        {
            //change the character animation by moving speed
            anim.SetFloat("speed", Mathf.Abs(movingSpeed));
            print(anim.GetFloat("speed"));

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

            //make character jump by adding force
            GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, jumpForce, 0.0f));
        }
    }

    // Update is called once per frame
    void Update () {

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
        if (col.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }
}
