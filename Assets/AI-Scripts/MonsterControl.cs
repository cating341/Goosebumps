using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MonsterControl : MonoBehaviour {

    [SerializeField]
    private List<GameObject> ground;
    [SerializeField]
    private List<GameObject> ladder;
    [SerializeField]
    private GameObject player;

    private Monster monster;

    private int climbingLadder;

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
        monster.MoveHor(ChaseTarget());
    }

    private Vector3 ChaseTarget()
    {
        GameObject target = DetermineTarget();
        Vector3 currentFloorTargetPosition = new Vector3();
        int upDown = CheckUpDown(target);
        if (upDown != 0)
        {
            this.climbingLadder = FindLadder(upDown);
            if (this.climbingLadder >= 0)
                currentFloorTargetPosition = this.ladder[this.climbingLadder].transform.position;
        }
        else
        {
            this.climbingLadder = -1;
            currentFloorTargetPosition = target.transform.position;
        }
        return currentFloorTargetPosition;
    }

    private int FindLadder(int upDown)
    {
        if ((upDown == 1 && this.monster.Floor == 1) || (upDown == -1 && this.monster.Floor == 2))
        {
            return 0;
        }
        else if((upDown == 1 && this.monster.Floor == 2) || (upDown == -1 && this.monster.Floor == 3))
        {
            return 1;
        }
        return -1;
    }

    private GameObject DetermineTarget()
    {
        if (this.attractions.Count == 0)
        {
            return this.player;
        }
        else
        {
            while( !CheckAvailable(this.attractions.Peek()))
            {
                this.attractions.Dequeue();
                print(this.attractions.Count);
            }
            print("thissss " + this.attractions.Count);
            return this.attractions.Count > 0 ? this.attractions.Peek() : this.player;
        }
    }

    private bool CheckAvailable(GameObject peek)
    {
        if (peek.tag == "Phone")
        {
            if (!peek.GetComponent<Animator>().GetBool("ring"))
            {
                return false;
            }
        }
        else if(peek.name == "TV")
        {
            if (!peek.GetComponent<Animator>().GetBool("on"))
            {
                return false;
            }
        }
        else if(peek.name == "Alarm clock")
        {
            if (!peek.GetComponent<Animator>().GetBool("ring"))
            {
                return false;
            }
        }
        return true;
    }

    private int CheckUpDown(GameObject target)
    {
        if (this.monster.Floor > target.GetComponent<AIInformation>().Floor)
        {
            return -1;
        }
        else if (this.monster.Floor < target.GetComponent<AIInformation>().Floor)
        {
            return 1;
        }
        return 0;
    }
    
    void OnTriggerStay(Collider col)
    {
        if (climbingLadder >= 0)
        {
            if (col.gameObject == this.ladder[this.climbingLadder])
            {
                int upDown = CheckUpDown(DetermineTarget());
                if ((upDown == 1 && this.monster.transform.position.y < this.ground[climbingLadder + 1].transform.position.y)
                    || (upDown == -1))
                {
                    this.monster.Climb(upDown, col);
                    Physics.IgnoreCollision(GetComponent<Collider>(), this.ground[climbingLadder + 1].GetComponent<Collider>());
                    GetComponent<Rigidbody>().useGravity = false;
                }
            }
        }
        
    }

    void OnTriggerExit(Collider col)
    {
        if (climbingLadder >= 0)
        {
            if (col.gameObject == this.ladder[climbingLadder])
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), this.ground[climbingLadder + 1].GetComponent<Collider>(), false);
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

                for (int i = 0; i < 3; i++)
                {
                    Physics.IgnoreCollision(GetComponent<Collider>(), this.ground[i].GetComponent<Collider>(), false);
                    GetComponent<Rigidbody>().useGravity = true;
                    if (col.gameObject == ground[i])
                    {
                        this.monster.Floor = i + 1;
                    }
                }
            }
        }
    }

    public void NewAttraction(GameObject obj)
    {
        this.attractions.Enqueue(obj);
    }

    public void Froze()
    {

    }
}
