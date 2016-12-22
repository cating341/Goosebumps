using UnityEngine;
using System.Collections;

public class JPoolObject : MonoBehaviour {
	
	public JPrefabPool pool{set;get;}
	// Use this for initialization
	public void DelayRecycle(float delay)
	{
		Invoke("Recycle",delay);
	}
	
	public void Recycle()
	{
		if(pool!=null)
		pool.Recycle(gameObject);
		else
		Destroy(gameObject);
	}
}
