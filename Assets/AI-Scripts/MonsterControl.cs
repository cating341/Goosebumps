using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MonsterControl : MonoBehaviour {

    [SerializeField]
    private GameObject ground1;
    [SerializeField]
    private GameObject ground2;
    [SerializeField]
    private GameObject ground3;
    [SerializeField]
    private GameObject player;

    private Monster monster;
    private int upDown;
    private string ladder1name = "LadderClimbable1";
    private string ladder2name = "LadderClimbable2";

    private Queue<GameObject> attractions;

    // Use this for initialization
    void Start()
    {
        this.monster = GetComponent<Monster>();
        this.attractions = new Queue<GameObject> ();
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
            target = this.player.transform.position;
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
        int targetFloor = new int();
        if (this.attractions.Count == 0)
        {
            targetFloor = this.player.GetComponent<Character> ().Floor;
        }
        else
        {
            targetFloor = this.attractions.Peek().GetComponent<AIInformation> ().Floor;
        }
        int floor = this.monster.Floor;
        if (floor > targetFloor)
        {
            this.upDown = -1;
        }
        else if (floor < targetFloor)
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
                    ground = ground2.GetComponent<Collider>();
                    Physics.IgnoreCollision(GetComponent<Collider>(), ground);
                    this.monster.Climb(this.upDown, col);
                }
                else if ((this.upDown == 1 && col.gameObject.name == ladder2name && this.monster.Floor != 3) 
                    || (this.upDown == -1 && col.gameObject.name == ladder2name && this.monster.Floor != 2))
                {
                    ground = ground3.GetComponent<Collider>();
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
            Physics.IgnoreCollision(GetComponent<Collider>(), ground2.GetComponent<Collider>(), false);
            Physics.IgnoreCollision(GetComponent<Collider>(), ground3.GetComponent<Collider>(), false);
            GetComponent<Rigidbody>().useGravity = true;
        }

    }
    

    void OnCollisionEnter(Collision col)
    {
        print(col.gameObject.name);
        if (col.gameObject.tag == "Ground")
        {
            if (col.transform.position.y < transform.position.y)
            {
                this.monster.OnGround = true;
                this.monster.Climbing = false;
            }
            if (col.gameObject == ground1)
            {
                this.monster.Floor = 1;
            }
            else if (col.gameObject == ground2)
            {
                this.monster.Floor = 2;
            }
            else if (col.gameObject == ground3)
            {
                this.monster.Floor = 3;
            }
        }
    }

    public void NewAttraction(GameObject obj)
    {
        this.attractions.Enqueue(obj);
        print(attractions.Peek());
    }
}
