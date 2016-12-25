using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
[RequireComponent (typeof (CanvasGroup))]
public class AnimatableCanvas : MonoBehaviour {
    public bool isAnimating = false;
    public float duration;
    CanvasGroup canvasGroup;
	protected virtual void Awake()
	{
        canvasGroup = GetComponent<CanvasGroup>();
        if(canvasGroup==null)
        {
            gameObject.AddComponent<CanvasGroup>();
        }


	}

    
	
    public virtual void hide(OnCompleteDelegate completeEvent=null)
    {
        hide(duration);
    }
	public void hide(float duration,OnCompleteDelegate completeEvent=null)
	{   
            if(!canvasGroup)
            {
                canvasGroup = GetComponentsInChildren<CanvasGroup>(true)[0];
            }
            
            if(canvasGroup)
            {
                //print(canvasGroup.DOKill(true));

                canvasGroup.DOFade(0,duration).OnComplete(
                ()=>{
                    if(completeEvent!=null)
                      completeEvent();
                    gameObject.SetActive(false);
                    print(name+"hide complete");
                });
            }
            else{
                Debug.LogError(name+"CanvasGroup not exist");
            }
            
        
	}
	
    public virtual void show(OnCompleteDelegate completeEvent=null)
    {
        show(duration);
    }
	public void show(float duration,OnCompleteDelegate completeEvent=null)
	{
        print(name+" show start");
        if(canvasGroup==null)
            canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.DOKill(true);
        canvasGroup.alpha = 0;
        activate();
        if(canvasGroup)
        {
            canvasGroup.DOFade(1,duration).OnComplete(
            ()=>{
                print(name+"show complete");
                
                if(completeEvent!=null)
                completeEvent();
            });
        }
        else{
            Debug.LogError("No canvas Group!");
        }
         
        
	}
    
	protected virtual void activate()
	{
		gameObject.SetActive(true);
	}
}
public delegate void OnCompleteDelegate();