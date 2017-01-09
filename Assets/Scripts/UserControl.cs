using System;
using UnityEngine;
using System.Collections;

public class UserControl {
	public static bool UseItem {
		get {
			return Input.GetKeyDown (KeyCode.Z);
		}
	}

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
			return Input.GetButtonUp ("Fire1");
		}
	}

	public static bool DecreaseTemp {
		get {
			return Input.GetButtonUp ("Fire2");
		}
	}
}
