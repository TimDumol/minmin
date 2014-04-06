using UnityEngine;
using System.Collections;

public class DoorOpener : MonoBehaviour {

	// Three private variables to store the state of the door,
	// a timer, and the current door collided with
	private bool doorIsOpen = false;
	private float doorTimer = 0.0f;
	private GameObject currentDoor;
	
	// Three public variables to allow us to set the length
	// of time the door will stay open and the sounds used
	float doorOpenTime = 3.0f;
	public AudioClip doorOpenSound;
	public AudioClip doorCloseSound;
	
	void Update () {
		
		// if the Ctrl button is pressed
		if (Input.GetButtonDown("Fire1") ) {
			Debug.Log ("Pressed key");
			// Declare a private variable of type RaycastHit for our ray
			RaycastHit hit;
			// Cast a ray 1 game unit (metre) forward from our position			}
			if(Physics.Raycast(transform.position, transform.forward, out hit, 4)){
				Debug.Log ("Raycast is hit");
				// if it hits a collider tagged "door" and the door is not open
				if(hit.collider.gameObject.tag == "InteractiveDoor" && doorIsOpen == false){
					Debug.Log (hit.collider.gameObject.tag);
					
					// store the gameObject of the collider hit
					currentDoor = hit.collider.gameObject;
					
					// Call the Door() function with parameters for:
					// sound to play, doorIsOpen, animation, currentDoor gameobject
					Door(doorOpenSound, true, "dooropen", currentDoor);
					Debug.Log("Door was opened");

				}   
			}
		}
		
		
		// timer to close the door after the specified time
		if(doorIsOpen){
			doorTimer += Time.deltaTime;
			
			if(doorTimer > doorOpenTime){
				
				// Call the door function again with parameters to close the door
				Door(doorCloseSound, false, "doorclose", currentDoor);
				Debug.Log("Door was closed");
				// reset the timer
				doorTimer = 0.0f;
			}
		}
	}
	
	
	// The Open and Close Door function
	
	void Door(AudioClip aClip, bool openCheck, string animName, GameObject thisDoor){
		
		// Play the sound assigned to the doorOpenSound/doorCloseSound variable
		audio.PlayOneShot(aClip);
		
		// Set the doorIsOpen variable to true or false
		doorIsOpen = openCheck;
		
		// Play the animation clip (animName) for this door
		thisDoor.animation.Play(animName);
	}
}
