using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

    private float maxSpeed = 0.05f;

    private int upDown;
    private float climbSpeed = 0.1f;

    private int floor;
    public int Floor
    {
        get
        {
            return this.floor;
        }
        set
        {
            this.floor = value;
        }
    }

    private bool onGround;
    public bool OnGround
    {
        get
        {
            return this.onGround;
        }
        set
        {
            this.onGround = value;
        }
    }

	private bool disabled;
	public bool Disabled 
	{
		get {
			return this.disabled;
		}
		set {
			this.disabled = value;
		}
	}

    Animator anim;
    void Start ()
    {
        this.upDown = 0;
        this.anim = GetComponent<Animator>();
        this.OnGround = false;
    }

	void Update() {
		
	}	

    public void MoveHor(Vector3 target)
    {
        float xDifference = target.x - transform.position.x;
        float way = Mathf.Abs(xDifference) < 0.5 ? 0 : xDifference / Mathf.Abs(xDifference);
        //this.Climbing = Mathf.Abs(xDifference) < 0.5 ? true : false;
		if (this.OnGround && !this.Disabled)
        {
            this.anim.SetFloat("speed", Mathf.Abs(way));
			MonsterMovement (way);
			CheckFacingSide (way);
        }
    }

    public void Climb(int upDown, Collider ladder)
    {
		if (!this.Disabled)
        {
            transform.position = new Vector3(ladder.transform.position.x, transform.position.y + climbSpeed * upDown, transform.position.z);
        }
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

	private void MonsterMovement(float way) {
		transform.position += new Vector3(way * maxSpeed, GetComponent<Rigidbody>().velocity.y * Time.deltaTime, 0);
	}
}
