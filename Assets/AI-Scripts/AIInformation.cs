using UnityEngine;
using System.Collections;

public class AIInformation : MonoBehaviour {
    
    [SerializeField]
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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
