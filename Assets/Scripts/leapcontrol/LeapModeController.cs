using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LeapMode{
	EditingMode,
	RotatingOjbect,
	NoRotatingOjbectInScene
}
public class LeapModeController : MonoBehaviour {
	public LeapMode mode;

	private static LeapModeController _instance;

	public static LeapModeController instance()
	{
		return _instance;
	}
	// Use this for initialization
	void Start () {
		_instance = this;
		mode = LeapMode.EditingMode;
	}
	

}
