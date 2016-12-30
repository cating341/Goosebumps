using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransitionController : MonoBehaviour {
	GameObject animateCanvas;
	CompositeText ct;
    Animator anim;

    public Sprite[] scene1;
    public int thisScene = 1; //0: gamescene0, 1: gamescene1
    private int state = 1, stateFinish;

    Image image;
    // Use this for initialization
    void Start () {
		animateCanvas = GameObject.Find ("AnimateCanvas");
        anim = GameObject.Find("BackgroundCanvas").GetComponent<Animator>();
        ct = animateCanvas.GetComponent<CompositeText> ();
		ct.color = Color.white;
		//Invoke ("animateString", 1.0f);
        if(thisScene == 0)
        {
            image = GameObject.Find("BackgroundCanvas/Image").GetComponent<Image>();
            stateFinish = 5;
        }
        else
        {
            stateFinish = 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
            if (state != -1)
            {          
                if (ct.isCompleted == false)
                {
                    print("do animate, state = " + state);
                    animateString();
                    gameObject.GetComponent<AudioSource>().Play();
                }
                else
                {
                    if (state == stateFinish) {
                        state = -1;
                        StartGame();
                    }
                    else {
                        state++;
                        Debug.Log("state = " + state);
                        animateString();
                        gameObject.GetComponent<AudioSource>().Play();
                    }
                }
            }
            
		}

        if (ct.isCompleted)
        {
            gameObject.GetComponent<AudioSource>().Stop();
        }
	}

	void animateString() {
        if (thisScene == 0)
        {
            switch (state)
            {
                case 1:
                    anim.SetInteger("flag", 1);
                    ct.DOText("Bump是位平凡的小男孩，\n有著愛著他的家人與溫暖的家\n但Bump有著不為人知的秘密，\n就是他擁有操縱溫度的能力。");
                    break;
                case 2:
                    ct.DOText("從小爸媽就告訴Bump，\n不要在別人面前使用這個能力\n才能保護自己不被排擠欺負。");
                    break;
                case 3:
                    ct.DOText("這天全家人正準備出門踏青，\n邪惡的魔女突然現身在面前。");
                    anim.SetInteger("flag", 2);
                    break;
                case 4:
                    image.sprite = scene1[1];
                    anim.SetInteger("flag", 1);
                    GameObject.Find("BackgroundCanvas/background").GetComponent<Image>().color = new Color(0.95f, 0.85f, 1);
                    image.rectTransform.localPosition = new Vector3(184, 34, 0);
                    ct.DOText("魔女平常隱身在黑暗的魔法森林，\n魔女欣賞Bump可以控制溫度的能力，\n想將招攬他成為自己的手下。");
                    break;
                case 5:
                    ct.DOText("為了不讓自己心愛的兒子被魔女抓走，\nBump的父母死命的抵抗，\n魔女一氣之下將Bump的父母變成了殭屍，\n並洗腦他們協助自己抓住Bump\n為了躲避變成怪物的家人，\nBump逃回了家中.....");                
                    break;

            }

            //ct.DOText("Bump在街道上遇到了魔女, \n魔女欣賞bump可以控制溫度的能力, \n將他的家人變成殭屍, \n藉此逼迫bump成為他的手下, \n為了躲避變成怪物的家人, \nbump逃回了家中.....");
        }
        else if (thisScene == 1)
            ct.DOText("Bump成功將家人關在房子內. \n為了解救被變為殭屍的家人們, \nBump決定前往魔法森林尋找魔女, \n與魔女決一死戰....");
	}

	public void StartGame()
	{
        if (thisScene == 0)
            Application.LoadLevel(GameObject.Find("SceneManager").GetComponent<MySceneManager>().PREVIEW1); 
        else if (thisScene == 1)
            Application.LoadLevel(GameObject.Find("SceneManager").GetComponent<MySceneManager>().PREVIEW2); 
	}
}
