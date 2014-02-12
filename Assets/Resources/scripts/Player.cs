using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	const float SPEED = 6;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision) {
		GameObject obj = collision.gameObject;
		Vector3 relativeVelocityVector = collision.relativeVelocity;
		if (relativeVelocityVector.sqrMagnitude > 5) {
			print ("COLLISIONENTER" + this.name + " collided with " + obj.name + "at a magnitude of " + relativeVelocityVector.sqrMagnitude);
			MasterController.endGame (false, "You have been crushed by falling objects.");
		}

	}

	void OnControllerColliderHit(ControllerColliderHit controllerColliderHit) {
		GameObject obj = controllerColliderHit.gameObject;
		/**
		if (obj.name == "boulder") {
			MasterController.endGame (false, "A boulder has crushed you and reduced you to nothing but organs.");
		}
		else */ 
		if (obj.name == "Door") {
			MasterController.endGame (true, null);
		} else if (obj.name == "Window") {
			MasterController.endGame (false, "Oops. Stay away from windows during an earthquake or fire!");
		} else if (obj.name == "tableUnder") {
			Destroy (obj);
			MasterController.ShowMessage("Protected yourself from falling debris by hiding under a table!");
			LevelCountdown.AddTime (5);
		} else if (obj.name == "shelfCube") {
			MasterController.ShowMessage("Stay away from shelves! Stuff might fall on you!");
		}
    }

	void OnTriggerEnter(Collider other) {
		GameObject obj = other.gameObject;
		if (obj.name == "Cross") {
			Destroy(obj);
			MasterController.ShowMessage("Picked up a First Aid Kit! Time + 10");
			LevelCountdown.AddTime(10);
		} else if (obj.name == "flames" || obj.name == "Small explosion") {
			MasterController.ShowMessage("Stay away from fires!!!");
			LevelCountdown.AddTime(-10);
		} 
	}

}
