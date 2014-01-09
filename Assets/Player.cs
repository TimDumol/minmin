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
		if (obj.name == "Boulder") {
			MasterController.endGame (false, "A boulder has crushed you and reduced you to nothing but organs.");
		}
    }
}
