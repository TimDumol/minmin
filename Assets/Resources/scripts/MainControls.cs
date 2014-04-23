using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainControls : MonoBehaviour {

	/*
	public static string[][] KeyStrings = new string[][]{
		// Restart
		new string[]{ "r", "joystick button 7" },
		// Open Door
		new string[]{ "f", "joystick button 0" },
		// Next Level
		new string[]{ "return", "joystick button 0" },
		// Jump
		new string[]{ "space", "joystick button 1" }
	}
	*/

	public static Dictionary <MasterController.Key, string[]> KeyStrings = new Dictionary<MasterController.Key, string[]>();

	// Use this for initialization
	void Start () {
		KeyStrings.Clear ();
		KeyStrings.Add ( MasterController.Key.Restart, new string[]{ "r", "joystick button 2" } );
		KeyStrings.Add ( MasterController.Key.OpenDoor, new string[]{ "f", "joystick button 0" } );
		KeyStrings.Add ( MasterController.Key.NextLevel, new string[]{ "return", "joystick button 0" } );
		KeyStrings.Add ( MasterController.Key.Jump, new string[]{ "space", "joystick button 1" } );
	}

	public static bool Check( MasterController.Key k ){
		// Debug.Log (k.ToString () + (int)k);
		if (KeyStrings.ContainsKey (k)) {
			if( Input.GetKeyDown ( KeyCode.JoystickButton2 ) ) Debug.Log ( "GG" );
			foreach (string s in KeyStrings[k]) {

				if (Input.GetKeyDown (s))
						return true;
			}
		}
		return false;
	}


	
	// Update is called once per frame
	void Update () {
	}
}
