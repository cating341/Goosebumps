using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

    public float maxSpeed = 0.1f;

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
        this.anim = GetComponent<Animator>();
        this.groundRadius = 0.1f;
        this.onGround = false;
    }

    public void Move(float movingSpeed, bool jump)
    {
        if (this.onGround)
        {
            this.anim.SetFloat("speed", Mathf.Abs(movingSpeed));
			MonsterMovement (movingSpeed);
			CheckFacingSide (movingSpeed);
        }
    }

    void Update () {

    }


	private void CheckFacingSide(float movingSpeed)
    {
		if (movingSpeed > 0) {
			transform.eulerAngles = new Vector3(0, 90, 0);
		} else if (movingSpeed < 0) {
			transform.eulerAngles = new Vector3(0, -90, 0);
		} else {
			transform.eulerAngles = new Vector3(0, 180, 0);
		}
    }

	private void MonsterMovement(float movingSpeed) {
		transform.position += new Vector3(movingSpeed * maxSpeed, GetComponent<Rigidbody>().velocity.y * Time.deltaTime, 0);

		print (GameObject.Find("Character").transform.position.x);

	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
			print ("hi");
            onGround = true;
        }
    }
}
