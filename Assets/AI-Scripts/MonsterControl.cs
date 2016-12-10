using UnityEngine;
using System.Collections;

public class MonsterControl : MonoBehaviour {

    private Monster monster;
    private int upDown;

    private string ground1name = "Floor";
    private string ground2name = "Floor (1)";
    private string ground3name = "Floor (2)";
    private string ladder1name = "LadderClimbable1";
    private string ladder2name = "LadderClimbable2";

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
        monster.MoveHor(FindChasingTarget());
    }

    private Vector3 FindChasingTarget()
    {
        CheckUpDown();
        Vector3 target = new Vector3();
        if (this.upDown != 0)
        {
            target = FindLadder();
        }
        else
        {
            target = GameObject.Find("Character").transform.position;
        }
        return target;
    }

    private Vector3 FindLadder()
    {
        Vector3 ladderPosition = new Vector3();
        int floor = this.monster.Floor;
        if (this.upDown == 1)
        {
            if (floor == 1)
            {
                ladderPosition = GameObject.Find(ladder1name).transform.position;
            }
            else if (floor == 2)
            {
                ladderPosition = GameObject.Find(ladder2name).transform.position;
            }
        }
        else
        {
            if (floor == 3)
            {
                ladderPosition = GameObject.Find(ladder2name).transform.position;
            }
            else if (floor == 2)
            {
                ladderPosition = GameObject.Find(ladder1name).transform.position;
            }
        }
        return ladderPosition;
    }

    private void CheckUpDown()
    {
        int characterFloor = GameObject.Find("Character").GetComponent<Character>().floor;
        int floor = this.monster.Floor;
        if (floor > characterFloor)
        {
            this.upDown = -1;
        }
        else if (floor < characterFloor)
        {
            this.upDown = 1;
        }
        else
        {
            this.upDown = 0;
        }
    }
    
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            if (this.upDown != 0)
            {
                GetComponent<Rigidbody>().useGravity = false;
                Collider ground = new Collider();
                if ((this.upDown == 1 && col.gameObject.name == ladder1name && this.monster.Floor != 2) 
                    || (this.upDown == -1 && col.gameObject.name == ladder1name && this.monster.Floor != 1))
                {
                    print("errrrr");
                    ground = GameObject.Find(ground2name).GetComponent<Collider>();
                    Physics.IgnoreCollision(GetComponent<Collider>(), ground);
                    this.monster.Climb(this.upDown, col);
                }
                else if ((this.upDown == 1 && col.gameObject.name == ladder2name && this.monster.Floor != 3) 
                    || (this.upDown == -1 && col.gameObject.name == ladder2name && this.monster.Floor != 2))
                {
                    ground = GameObject.Find(ground3name).GetComponent<Collider>();
                    Physics.IgnoreCollision(GetComponent<Collider>(), ground);
                    this.monster.Climb(this.upDown, col);
                }
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            Collider ground = new Collider();
            if ((this.upDown == 1 && col.gameObject.name == ladder1name) || (this.upDown == -1 && col.gameObject.name == ladder1name))
            {
                print("oh no");
                ground = GameObject.Find(ground2name).GetComponent<Collider>();
                Physics.IgnoreCollision(GetComponent<Collider>(), ground, false);
                GetComponent<Rigidbody>().useGravity = true;
            }
            else if ((this.upDown == 1 && col.gameObject.name == ladder2name) || (this.upDown == -1 && col.gameObject.name == ladder2name))
            {
                print("oh hey");
                ground = GameObject.Find(ground3name).GetComponent<Collider>();
                Physics.IgnoreCollision(GetComponent<Collider>(), ground, false);
                GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }



    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            if (col.transform.position.y < transform.position.y)
            {
                this.monster.OnGround = true;
                this.monster.Climbing = false;
            }
            if (col.gameObject.name == ground1name)
            {
                this.monster.Floor = 1;
            }
            else if (col.gameObject.name == ground2name)
            {
                this.monster.Floor = 2;
            }
            else if (col.gameObject.name == ground3name)
            {
                this.monster.Floor = 3;
            }
        }
    }
}
