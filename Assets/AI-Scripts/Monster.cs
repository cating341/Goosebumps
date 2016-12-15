using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster : MonoBehaviour {


    private float maxSpeed;

    private int upDown;
    private float climbSpeed = 0.1f;
	private bool dead;

	private Dictionary<string, bool> disabledList;

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

    Animator anim;
    void Start ()
    {
        this.upDown = 0;
        this.anim = GetComponent<Animator>();
        this.OnGround = false;
		this.dead = false;
		if (GameObject.Find("SceneManager").GetComponent<MySceneManager>().GameSceneIsPreview())
        {
            this.maxSpeed = 0;
        }
        else
        {
            this.maxSpeed = 0.05f;
        }
		this.disabledList = new Dictionary<string, bool> ();
    }

	void Update() {
		if (GetComponent<Animator> ().GetBool ("dead") && !this.dead) {
			this.dead = true;
			Invoke ("DestroyMe", 4.0f);
		}
	}	

	public bool CheckDisability() {
		bool disabled = false;
		foreach (KeyValuePair<string, bool> item in this.disabledList) {
			if (item.Value) {
				disabled = true;
				break;
			}
		}
		return disabled;
	}

	private void DestroyMe() {
		Destroy (gameObject);
	}

    public void MoveHor(Vector3 target)
    {
        float xDifference = target.x - transform.position.x;
        float way = Mathf.Abs(xDifference) < 0.5 ? 0 : xDifference / Mathf.Abs(xDifference);

		if (this.OnGround && !CheckDisability())
        {
            this.anim.SetFloat("speed", Mathf.Abs(way));
			MonsterMovement (way);
			CheckFacingSide (way);
        }
    }

    public void Climb(int upDown, Collider ladder)
    {
		if (!CheckDisability())
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

	public void NewDisability(string key, bool value) {
		print (key);
		disabledList[key] = value;
	}
}
