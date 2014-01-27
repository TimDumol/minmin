using UnityEngine;
using System.Collections;

public class ContactKillController : MonoBehaviour {
	void Start () {

	}
	
	void Update () {
	
	}

	void OnCollisionEnter (Collision collision) {
		//print (this.name + " collided with " + collision.gameObject.name);
	}

	void OnTriggerEnter (Collider collider) {
		//print (collider.gameObject.name);
		//Vector3 normal = collider.contacts[0].normal;
		GameObject obj = collider.gameObject;
		if (obj.name == "First Person Controller") {
			//print (obj.name + " collided with " + this.name + " and the magnitude is " + collisionInfo.relativeVelocity.magnitude);
		}	
	}
}
