using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {
	//CONSTANTS
	const float SPEED = 6;

	//SOUNDS
	public AudioClip successAudio;
	public AudioClip gameOverAudio;
	public AudioClip nextLevelAudio;

	public AudioClip do_not_use_the_elevator;
	public AudioClip picked_up_a_first_aid_kit;
	public AudioClip protected_yourself;
	public AudioClip stay_away_from_fires;
	public AudioClip stay_away_from_windows;
	public AudioClip you_have_been_crushed_by_falling;

	private bool isCrawling = false;

	void OnCollisionEnter(Collision collision) {
		//GameObject obj = collision.gameObject;
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
			MasterController.ShowMessage("");
			LevelCountdown.AddTime (10);
		}
    }
	
	void OnTriggerEnter(Collider other) {
		GameObject obj = other.gameObject;
		if (obj.name == "Cross") {
			AudioSource.PlayClipAtPoint(successAudio, transform.position); 
			Destroy(obj);

			AudioSource.PlayClipAtPoint(picked_up_a_first_aid_kit, transform.position, 1.0f); 
			MasterController.ShowMessage("");
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

			MasterController.ShowMessage("");
			LevelCountdown.AddTime(-1f);
		} 
	}

	void Start(){
	}

	void Update () {

		if(Input.GetKeyDown("c") && isCrawling){
			// this.transform.position += Vector3.up;
			isCrawling = !isCrawling;
		}
		else if (Input.GetKeyDown("c") && !isCrawling) {
			// this.transform.position += Vector3.down;
			isCrawling = !isCrawling;
		}
	}

}
