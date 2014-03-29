using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	//CONSTANTS
	const float SPEED = 6;
	const float FIRE_THRESHOLD = 0.5f;
	const float SHAKE_THRESHOLD = 15f;
	const float SHAKE_TIMES_THRESHOLD = 4f;

	static float fireCounter = 0f;
	static float shakeCounter = 0f;
	static Quaternion lastRotation = new Quaternion ();
	//SOUNDS
	public AudioClip successAudio;
	public AudioClip gameOverAudio;
	public AudioClip nextLevelAudio;

	public static void Reset() {
		fireCounter = shakeCounter = 0f;
		lastRotation = new Quaternion ();
	}

	void Update() {
		if (fireCounter >= FIRE_THRESHOLD) {
			MasterController.ShowMessage("You are on fire! Get out of danger, then stop, drop, and roll! (Shake your head!)");
		}
	}

	void FixedUpdate() {
		if (fireCounter >= FIRE_THRESHOLD) {
			// Trigger fire damage
			LevelCountdown.AddTime(-5e-2f);

			// Handle shaking mechanism
			Quaternion rot = transform.Find ("Main Camera").gameObject.transform.rotation;
			float angle = Mathf.Abs(Quaternion.Angle(rot,lastRotation));
			if (angle >= SHAKE_THRESHOLD) {
				shakeCounter += 1;
				if (shakeCounter >= SHAKE_TIMES_THRESHOLD) {
					fireCounter = 0f;
					shakeCounter = 0f;
				}
			}
			lastRotation = rot;
		}
	}

	void OnCollisionEnter(Collision collision) {
		GameObject obj = collision.gameObject;
		Vector3 relativeVelocityVector = collision.relativeVelocity;
		if (relativeVelocityVector.sqrMagnitude > 5) {
			AudioSource.PlayClipAtPoint(gameOverAudio, transform.position, 3.0f); 
			MasterController.endGame (false, "You have been crushed by falling objects.");
		}
	}

	void OnControllerColliderHit(ControllerColliderHit controllerColliderHit) {
		GameObject obj = controllerColliderHit.gameObject;
		if (obj.name == "Door") {
			AudioSource.PlayClipAtPoint(nextLevelAudio, transform.position, 2.0f); 
			MasterController.endGame (true, null);
		} else if (obj.name == "Window") {
			AudioSource.PlayClipAtPoint(gameOverAudio, transform.position, 3.0f); 
			MasterController.endGame (false, "Oops. Stay away from windows during an earthquake or fire!");
		} else if (obj.name == "tableUnder") {
			AudioSource.PlayClipAtPoint(successAudio, transform.position); 
			Destroy (obj);
			MasterController.ShowMessage("Protected yourself from falling debris by hiding under a table! Time +10");
			LevelCountdown.AddTime (10);
		} else if (obj.name == "shelfCube") {
			MasterController.ShowMessage("Stay away from shelves! Stuff might fall on you!");
			LevelCountdown.AddTime (-0.5f);
		
		}
    }

	void OnTriggerEnter(Collider other) {
		GameObject obj = other.gameObject;
		if (obj.name == "Cross") {
			AudioSource.PlayClipAtPoint(successAudio, transform.position); 
			Destroy(obj);
			MasterController.ShowMessage("Picked up a First Aid Kit! Time + 15");
			LevelCountdown.AddTime(15);
		}
	}

	void OnParticleCollision(GameObject other) {
		GameObject obj = other.gameObject;
		bool isFire = obj.CompareTag("Fire");
		if (!isFire && obj.transform.parent != null) {
			isFire =  obj.transform.parent.gameObject.CompareTag("Fire");
		}
		if (isFire) {
			MasterController.ShowMessage("Stay away from fires!!!");
			LevelCountdown.AddTime(-1f);
			fireCounter += 1;
		}
	}

}
