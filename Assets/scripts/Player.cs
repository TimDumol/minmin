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
		else */ if (obj.name == "Door") {
			MasterController.endGame (true, null);
		}
    }

}
