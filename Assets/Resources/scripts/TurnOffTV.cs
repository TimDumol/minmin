using UnityEngine;
using System.Collections;

public class TurnOffTV : MonoBehaviour {
	
	
	// Smothly open a door
	private bool isOn;
	private bool inArea;

	public Texture theTVOff;
	public Light myLight;	
	public AudioClip successAudio;

	void Start() {
		myLight = light.GetComponent("Light") as Light;
		isOn = true;
	}

	//Main function
	void Update () {
		// if(Input.GetKeyDown("f") && enter){
		if( isOn && inArea && MainControls.Check ( MainControls.Key.OpenDoor ) ){
			myLight.enabled = !myLight.enabled;
			renderer.material.mainTexture = theTVOff;
			HealthController.AddHealth(20);
			AudioSource.PlayClipAtPoint(successAudio, transform.position); 
			isOn = false;
	
		}
	}
	
	void OnGUI() {	
		if(inArea && isOn){
			GUI.Label(new Rect(Screen.width/2 - 75, Screen.height - 100, 150, 30), "Press A (key F) to unplug the TV");
		}
	}
	
	//Activate the Main function when player is near the door
	void OnTriggerEnter (Collider other){
		// Debug.Log (other.gameObject.name);
		if (other.gameObject.tag == "Player" && isOn) {
			inArea = true;
		}
	}
	
	//Deactivate the Main function when player is go away from door
	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == "Player") {
			inArea = false;
		}
	}
}
