using UnityEngine;
using System.Collections;

public class ContactKillController : MonoBehaviour {
	//SOUNDS
	public AudioClip thumpAudio;
	bool hasNotCollided = true;


	//Play the sound of impact once
	void OnCollisionEnter(Collision collision) {
		GameObject obj = collision.gameObject;
		if (obj.name == "Floor" && hasNotCollided) {
			AudioSource.PlayClipAtPoint (thumpAudio, transform.position, 2.5f); 
			hasNotCollided = !hasNotCollided;
		}
	}

}
