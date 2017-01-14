using System;
using UnityEngine;
using System.Collections;

public class UserControl {
	public static bool UseItem {
		get {
			return Input.GetKeyDown (KeyCode.Q);
		}
	}

    public static KeyCode UseItemKey = KeyCode.Q;
    

	public static bool PickupItem {
		get {
			return Input.GetKeyDown (KeyCode.E);
		}
	}

	public static bool PutItem {
		get {
			return Input.GetKeyDown (KeyCode.R);
		}
	}

	public static bool IncreaseTemp {
		get {
			return Input.GetKeyDown (KeyCode.LeftShift);
		}
	}

	public static bool DecreaseTemp {
		get {
			return Input.GetKeyDown (KeyCode.LeftControl);
		}
	}

	public static bool GetGearsBack{
		get{ 
			return Input.GetKeyDown (KeyCode.B);
		}
	}

    public static bool PutAllGears
    {
        get
        {
            return Input.GetKeyDown(KeyCode.P);
        }
    }
}
