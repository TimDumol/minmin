using UnityEngine;
using System.Collections;
using System;

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
			Quaternion rot = transform.Find ("OVRCameraController").gameObject.transform.rotation;
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

	public AudioClip do_not_use_the_elevator;
	public AudioClip picked_up_a_first_aid_kit;
	public AudioClip protected_yourself;
	public AudioClip stay_away_from_fires;
	public AudioClip stay_away_from_windows;
	public AudioClip you_have_been_crushed_by_falling;

	void OnCollisionEnter(Collision collision) {
		GameObject obj = collision.gameObject;
		Vector3 relativeVelocityVector = collision.relativeVelocity;
		if (relativeVelocityVector.sqrMagnitude > 5) {
			AudioSource.PlayClipAtPoint(gameOverAudio, transform.position, 3.0f); 
			AudioSource.PlayClipAtPoint(you_have_been_crushed_by_falling, transform.position, 2.0f); 
			MasterController.endGame (false, "");
		}
	}

	void OnControllerColliderHit(ControllerColliderHit controllerColliderHit) {
		GameObject obj = controllerColliderHit.gameObject;
		if (obj.name == "Door") {
			AudioSource.PlayClipAtPoint(nextLevelAudio, transform.position, 2.0f); 
			MasterController.endGame (true, null);
		} else if (obj.name == "Window") {

			AudioSource.PlayClipAtPoint(gameOverAudio, transform.position, 3.0f); 
			AudioSource.PlayClipAtPoint(stay_away_from_windows, transform.position, 2.0f); 
			MasterController.endGame (false, "");
		} else if (obj.name == "tableUnder") {
			AudioSource.PlayClipAtPoint(successAudio, transform.position); 
			Destroy (obj);
			AudioSource.PlayClipAtPoint(protected_yourself, transform.position, 3.0f); 
			//MasterController.ShowMessage("");
			LevelCountdown.AddTime (10);
		}
    }
	
	void OnTriggerEnter(Collider other) {
		GameObject obj = other.gameObject;
		if (obj.name == "Cross") {
			AudioSource.PlayClipAtPoint(successAudio, transform.position); 
			Destroy(obj);

			AudioSource.PlayClipAtPoint(picked_up_a_first_aid_kit, transform.position, 1.0f); 
			//MasterController.ShowMessage("");
			LevelCountdown.AddTime(15);
		}
	}

	DateTime lastFire = new DateTime(0);

	void OnParticleCollision(GameObject other) {
		GameObject obj = other.gameObject;
		bool isFire = obj.CompareTag("Fire");
		if (!isFire && obj.transform.parent != null) {
			isFire =  obj.transform.parent.gameObject.CompareTag("Fire");
		}
		if (isFire) {
			DateTime now = DateTime.Now;
			AudioSource fireAudioSource = GetComponent<AudioSource>();
			if ((now - lastFire).Seconds >= 1) {
				if (!fireAudioSource.isPlaying) {
					fireAudioSource.Play();
				}
			}
			lastFire = now;

			//MasterController.ShowMessage("");
			LevelCountdown.AddTime(-0.4f);
			fireCounter += 1;
		}
	}

}
