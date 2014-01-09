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

	void OnControllerColliderHit(ControllerColliderHit collision) {
		GameObject obj = collision.gameObject;
		print ("Collided with " + obj.name);
		if (obj.name == "boulder") {
			MasterController.endGame (false, "A boulder has crushed you and reduced you to nothing but organs.");
		}
		else if (obj.name == "Door") {
			MasterController.endGame (true, null);
		}
    }
}
