using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainControls : MonoBehaviour {


	public static Dictionary <string, string> Xbox = new Dictionary<string, string>();
	public static Dictionary <MasterController.Key, string[]> KeyStrings = new Dictionary<MasterController.Key, string[]>();

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


		KeyStrings.Add ( MasterController.Key.Restart, new string[]{ "r", Xbox["x"] } );
		KeyStrings.Add ( MasterController.Key.OpenDoor, new string[]{ "f", Xbox["a"] } );
		KeyStrings.Add ( MasterController.Key.NextLevel, new string[]{ "return", Xbox["a"] } );
		KeyStrings.Add ( MasterController.Key.Jump, new string[]{ "space", Xbox["b"] } );
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
