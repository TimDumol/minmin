using UnityEngine;
using System.Collections;

public class DoorOpener : MonoBehaviour {

	
	// Smothly open a door
	float smooth = 2.0f;
	float DoorOpenAngle = 90.0f;
	private bool open;
	private bool enter;
	
	private Vector3 defaultRot;
	private Vector3 openRot;
	
	void Start() {
		defaultRot = transform.eulerAngles;
		openRot = new Vector3 (defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);
	}
	
	//Main function
	void Update () {
		if(open){
			transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
		} else{
			transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
		}
		// if(Input.GetKeyDown("f") && enter){
		if( enter && MainControls.Check ( MasterController.Key.OpenDoor ) ){
			open = !open;
		}
	}
	
	void OnGUI() {
		if(enter){
			GUI.Label(new Rect(Screen.width/2 - 75, Screen.height - 100, 150, 30), "Press A (key F) to open the door");
		}
	}
	
	//Activate the Main function when player is near the door
	void OnTriggerEnter (Collider other){
		// Debug.Log (other.gameObject.name);
		if (other.gameObject.tag == "Player") {
			enter = true;
		}
	}
	
	//Deactivate the Main function when player is go away from door
	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == "Player") {
			enter = false;
		}
	}
}
