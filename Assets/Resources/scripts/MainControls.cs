using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainControls : MonoBehaviour {

	// Add new Keys here

	public enum Key { Restart, OpenDoor, NextLevel, Jump, Crouch, Pause };

	public static Dictionary <string, string> Xbox = new Dictionary<string, string>();
	public static Dictionary <MainControls.Key, string[]> KeyStrings = new Dictionary<MainControls.Key, string[]>();

	// Use this for initialization
	void Start () {

		Xbox.Clear ();
		KeyStrings.Clear ();

		string[] xboxStrings = {
			"a", "b", "x", "y", "l", "r", "back", "start", "lstick", "rstick"
		};

		for( int i=0; i<xboxStrings.Length; ++i ){
			Xbox[ xboxStrings[i] ] = "joystick button " + i;
		}


		KeyStrings.Add ( Key.Restart, new string[]{ "r", Xbox["x"] } );
		KeyStrings.Add ( Key.OpenDoor, new string[]{ "f", Xbox["a"] } );
		KeyStrings.Add ( Key.NextLevel, new string[]{ "return", Xbox["a"] } );
		KeyStrings.Add ( Key.Jump, new string[]{ "space", Xbox["b"] } );
		KeyStrings.Add ( Key.Crouch, new string[]{ "c", Xbox["rstick"] } );
		KeyStrings.Add ( Key.Pause, new string[]{ "p", Xbox["start"] } );

		// KeyStrings.Add ( Key.*****, new string[]{ ****, ****, ... } )

	}

	// to use:
	// if( MainControls.Check( MainControls.Key.Restart ) ) ...

	public static bool Check( Key k, bool downOnly = true ){
		// Debug.Log (k.ToString () + (int)k);
		if (KeyStrings.ContainsKey (k)) {
			foreach (string s in KeyStrings[k]) {
				if (Input.GetKeyDown (s))
					return true;
				if( !downOnly && Input.GetKey (s) )
					return true;
			}
		}
		return false;
	}


	
	// Update is called once per frame
	void Update () {
	}
}
