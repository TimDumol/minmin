﻿using UnityEngine;
using System.Collections;

public class OnImpact : MonoBehaviour {
	//SOUNDS
	public AudioClip thumpAudio;

	void OnControllerColliderHit(ControllerColliderHit controllerColliderHit) {
		GameObject obj = controllerColliderHit.gameObject;
		if (obj.name == "Floor") {
			AudioSource.PlayClipAtPoint(thumpAudio, transform.position); 
		} 
	}
}
