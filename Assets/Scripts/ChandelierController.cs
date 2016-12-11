using UnityEngine;
using System.Collections; 
using UnityEngine.UI;

public class ChandelierController : MonoBehaviour {
     
    public GameObject numCanvas;
    public Text numText;

    private Animator animation;
    int hitting = 5;
    bool enable = true; 
	// Use this for initialization
	void Start () {
        animation = GetComponent<Animator>();
        numCanvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (enable && hitting <= 0)
        {
            //gameObject.transform.position -= new Vector3(0, 2.8f, 0);
            animation.SetBool("falldown", true);
            enable = false;
            Invoke("takeoffOutline", 1.2f);
        }
	}

    void OnTriggerStay(Collider other)
    { 
        if (enable && other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Z))
        {
            other.gameObject.GetComponent<Character>().Jump();
        }
    }

    void OnTriggerEnter(Collider other)
    { 
        if (enable && other.gameObject.tag == "Player")
        {
            numCanvas.SetActive(true);
            hitting--;
            numText.GetComponent<Text>().text = hitting.ToString();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (enable && other.gameObject.tag == "Player")
        {
            numCanvas.SetActive(false);
        }
    }

    void takeoffOutline()
    {
        numCanvas.SetActive(false);
        Destroy(gameObject.GetComponentInChildren<OutlineCustom>());
    }
}
