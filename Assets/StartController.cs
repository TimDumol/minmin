using UnityEngine;
using System.Collections;

public class StartController : MonoBehaviour {

	// Use this for initialization
	private bool hasNotStarted;
	void Start () {
		if (Application.isPlaying) {
			Time.timeScale = 0;		
			hasNotStarted = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if( MainControls.Check ( MasterController.Key.Jump ) && hasNotStarted){
			Time.timeScale = 1;
			hasNotStarted = false;
		}
		else if( MainControls.Check ( MasterController.Key.NextLevel )){
			Time.timeScale = 1;
			Application.LoadLevel(1);
		}
	}
}
